using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using FrameHeat.Commands;
using FrameHeat.Layout;
using FrameHeat.Native;
using FrameHeat.Sensors;
using FrameHeat.Services;
using FrameHeat.Startup;
using FrameHeat.ViewModels;
using FrameHeat.Views;

namespace FrameHeat.Hosting;

/// <summary>Creates the application's parts, connects them and carries out the user's commands.</summary>
internal sealed class AppController : IAppCommands, IDisposable
{
    // Settings changes arrive in bursts (a slider drag); they are written to disk once the burst settles.
    private static readonly TimeSpan SaveDelay = TimeSpan.FromMilliseconds(600);

    private readonly CommandLineOptions _options;
    private readonly Dispatcher _dispatcher = Application.Current.Dispatcher;
    private readonly SettingsStore _store;
    private readonly bool _firstRun;
    private readonly HardwareMonitor _monitor = new();
    private readonly DebouncedAction _saver;
    private readonly OverlayViewModel _viewModel;
    private readonly OverlayWindow _overlay;
    private readonly DemoRunner _demo;

    // Created in Start(), once the widget is on screen.
    private TrayIcon? _tray;
    private HotkeyService? _hotkeys;

    // At most one settings window; null while it is closed.
    private SettingsWindow? _settingsWindow;

    // Set while the controller itself flips StartWithWindows back, so that change is not applied twice.
    private bool _applyingAutoStart;
    private bool _disposed;

    // Loads the settings and builds the widget and its helpers; nothing is shown or started until Start().
    public AppController(CommandLineOptions options)
    {
        _options = options;
        _store = new SettingsStore(options.SettingsFile ?? SettingsStore.DefaultPath);
        _firstRun = !_store.Exists;
        Settings = _store.Load();

        _saver = new DebouncedAction(_dispatcher, SaveDelay, Save);
        _viewModel = new OverlayViewModel(Settings);
        _viewModel.SettingsRequested += ShowSettings;
        _overlay = new OverlayWindow(_viewModel, Settings.Placement);
        _demo = new DemoRunner(_dispatcher, _viewModel.Update, () => WindowTools.RefreshRateHz(IntPtr.Zero));
    }

    public AppSettings Settings { get; }

    // State read by the tray menu and the settings window.
    public SensorSnapshot? LatestSnapshot => _monitor.Latest;

    public SensorSnapshot? WidgetSnapshot => _viewModel.LastSnapshot;

    public bool IsVisible => _overlay.IsVisible;

    public bool IsEditMode => _viewModel.EditMode;

    public bool IsDemoRunning => _demo.IsRunning;

    /// <summary>Shows the widget and starts everything that runs alongside it: tray icon, hotkeys, sensors.</summary>
    public void Start()
    {
        // The scheduled task is the source of truth: a task removed by hand must not leave the setting on.
        Settings.StartWithWindows = AutoStartTask.IsEnabled();

        _overlay.Show();
        _tray = new TrayIcon(this);
        RegisterHotkeys();

        Settings.Changed += OnSettingsChanged;
        _monitor.SnapshotReady += OnSnapshot;
        _monitor.Start();

        // Development switches from the command line.
        if (_options.EditMode)
            _viewModel.EditMode = true;
        if (_options.OpenSettings)
            ShowSettings();

        // What to tell the user on startup: nothing in demo mode, the autostart question on the very
        // first run, and otherwise a tray hint unless Windows started the program itself.
        if (_options.Demo)
            _demo.Toggle();
        else if (_firstRun)
            _dispatcher.BeginInvoke(AskAboutAutoStart, DispatcherPriority.ApplicationIdle);
        else if (!_options.AutoStarted)
            _tray.ShowStartupHint();
    }

    // The methods below carry out the commands from the tray menu, the hotkeys and the settings window.
    public void ToggleVisible()
    {
        if (_overlay.IsVisible)
            _overlay.Hide();
        else
            _overlay.Show();
    }

    public void ToggleEditMode()
    {
        if (!_overlay.IsVisible)
            _overlay.Show();

        _viewModel.EditMode = !_viewModel.EditMode;
    }

    public void ToggleOrientation() => Settings.Appearance.ToggleOrientation();

    public void ToggleDemo() => _demo.Toggle();

    public void ToggleAutoStart() => Settings.StartWithWindows = !Settings.StartWithWindows;

    public void SnapTo(ScreenCorner corner) => _overlay.SnapTo(corner);

    public void ShowSettings()
    {
        if (_settingsWindow is null)
        {
            _settingsWindow = new SettingsWindow(this);
            _settingsWindow.Closed += (_, _) => _settingsWindow = null;
        }

        _settingsWindow.BringToFront();
    }

    /// <summary>Writes the sensor report next to the log and shows it in Explorer.</summary>
    public void SaveDiagnostics()
    {
        Directory.CreateDirectory(AppIdentity.LogDirectory);
        var path = Path.Combine(AppIdentity.LogDirectory, "diagnostics.txt");
        File.WriteAllText(path, _monitor.BuildReport());
        Process.Start("explorer.exe", $"/select,\"{path}\"")?.Dispose();
    }

    public void Exit()
    {
        Dispose();
        Application.Current.Shutdown();
    }

    // A hotkey that another program already owns is skipped (and logged); the rest still work.
    private void RegisterHotkeys()
    {
        var hotkeys = new HotkeyService();
        hotkeys.Register(Hotkeys.ToggleVisible, ToggleVisible);
        hotkeys.Register(Hotkeys.ToggleEditMode, ToggleEditMode);
        hotkeys.Register(Hotkeys.ToggleOrientation, ToggleOrientation);
        hotkeys.Register(Hotkeys.ShowSettings, ShowSettings);
        _hotkeys = hotkeys;
    }

    // Runs on the sensor thread; the demo check is a plain flag read.
    private void OnSnapshot(SensorSnapshot snapshot)
    {
        if (!_demo.IsRunning)
            _dispatcher.BeginInvoke(() => _viewModel.Update(snapshot));
    }

    // Any setting change ends up here. Start-with-Windows takes effect immediately; everything else
    // only needs saving, and the widget itself listens to the settings on its own.
    private void OnSettingsChanged(SettingsChange change)
    {
        if (change is { Area: SettingsArea.General, Property: nameof(AppSettings.StartWithWindows) })
        {
            if (!_applyingAutoStart)
                ApplyAutoStart();
            return;
        }

        _saver.Request();
    }

    // Creates or removes the logon task to match the setting.
    private void ApplyAutoStart()
    {
        var wanted = Settings.StartWithWindows;
        var succeeded = wanted ? AutoStartTask.Enable() : AutoStartTask.Disable();

        if (!succeeded)
        {
            // Creating the task needs administrator rights. Undo the toggle so the setting does not claim otherwise.
            MessageDialog.Warn(Loc.T("msg_autostart_failed"));

            _applyingAutoStart = true;
            Settings.StartWithWindows = !wanted;
            _applyingAutoStart = false;
        }

        _saver.Request();
    }

    // Asked once, on the very first run, and only when the task can actually be created.
    private void AskAboutAutoStart()
    {
        if (!SystemInfo.IsElevated())
            return;

        Settings.StartWithWindows = MessageDialog.Ask(Loc.T("msg_autostart_ask"));

        // Saved right away even when nothing changed, so that the settings file marks the first run as over.
        Save();
    }

    // A settings file that cannot be written must not take the program down; it is logged instead.
    private void Save()
    {
        try
        {
            _store.Save(Settings);
        }
        catch (Exception ex)
        {
            Log.Error("Saving the settings failed", ex);
        }
    }

    public void Dispose()
    {
        // Exit() and the application shutdown both end up here.
        if (_disposed)
            return;
        _disposed = true;

        _saver.Cancel();
        Save();
        _demo.Stop();
        _hotkeys?.Dispose();
        _tray?.Dispose();
        _monitor.Dispose();
    }
}
