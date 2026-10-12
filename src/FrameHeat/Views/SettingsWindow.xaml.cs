using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using FrameHeat.Commands;
using FrameHeat.Layout;
using FrameHeat.Native;
using FrameHeat.Sensors;
using FrameHeat.Services;
using FrameHeat.ViewModels;

namespace FrameHeat.Views;

/// <summary>
/// The settings window. It binds straight to the settings object, so most controls need no code
/// here; this file holds the buttons that trigger actions, the lists built from the machine and the live status text.
/// </summary>
internal sealed partial class SettingsWindow : Window
{
    private static readonly TimeSpan StatusRefreshInterval = TimeSpan.FromSeconds(2);

    private readonly IAppCommands _controller;
    private readonly DispatcherTimer _statusTimer;
    private readonly TileSlots _tileSlots;
    private readonly DiskSlots _diskSlots;

    public SettingsWindow(IAppCommands controller)
    {
        _controller = controller;

        InitializeComponent();
        DataContext = controller.Settings;

        _tileSlots = new TileSlots(controller.Settings.Tiles);
        _diskSlots = new DiskSlots(controller.Settings.Tiles);
        TileList.ItemsSource = _tileSlots.Items;
        DiskList.ItemsSource = _diskSlots.Items;

        // The status box (permissions, driver, detected devices) and the list of disks follow the sensors while the window is open.
        Refresh();
        _statusTimer = new DispatcherTimer(DispatcherPriority.Background, Dispatcher) { Interval = StatusRefreshInterval };
        _statusTimer.Tick += (_, _) => Refresh();
        _statusTimer.Start();
        Closed += (_, _) =>
        {
            _statusTimer.Stop();
            _tileSlots.Dispose();
            _diskSlots.Dispose();
        };
    }

    /// <summary>Shows the window on top of whatever is running, even a game or a browser with focus.</summary>
    public void BringToFront()
    {
        if (WindowState == WindowState.Minimized)
            WindowState = WindowState.Normal;

        Show();

        // Toggling Topmost lifts the window over other normal windows even if the focus change is refused.
        Topmost = true;
        Topmost = false;

        WindowTools.BringToFront(new WindowInteropHelper(this).EnsureHandle());
        Activate();
    }

    private void Refresh()
    {
        var snapshot = _controller.LatestSnapshot;
        var appearance = _controller.Settings.Appearance;

        StatusText.Text = StatusSummary.Build(snapshot, SystemInfo.IsElevated(), SystemInfo.IsPawnIoInstalled(), appearance.TemperatureUnit);
        _diskSlots.Refresh(_controller.WidgetSnapshot);
    }

    // Handlers for the buttons in the window; the work itself is done by the application controller.
    private void OnSaveDiagnostics(object sender, RoutedEventArgs e) => _controller.SaveDiagnostics();

    private void OnToggleDemo(object sender, RoutedEventArgs e) => _controller.ToggleDemo();

    private void OnToggleEditMode(object sender, RoutedEventArgs e) => _controller.ToggleEditMode();

    private void OnResetLimits(object sender, RoutedEventArgs e) => _controller.Settings.Alerts.ResetToDefaults();

    private void OnResetColors(object sender, RoutedEventArgs e) => _controller.Settings.Appearance.ResetAlertColors();

    // Each corner button carries its corner in Tag.
    private void OnSnap(object sender, RoutedEventArgs e) => _controller.SnapTo((ScreenCorner)((FrameworkElement)sender).Tag);

    // Each color button carries the name of the setting it changes in Tag.
    private void OnPickColor(object sender, RoutedEventArgs e)
    {
        var appearance = _controller.Settings.Appearance;

        switch (((FrameworkElement)sender).Tag)
        {
            case nameof(AppearanceSettings.AccentColor):
                appearance.AccentColor = Pick(appearance.AccentColor);
                break;
            case nameof(AppearanceSettings.WarningColor):
                appearance.WarningColor = Pick(appearance.WarningColor);
                break;
            case nameof(AppearanceSettings.CriticalColor):
                appearance.CriticalColor = Pick(appearance.CriticalColor);
                break;
        }
    }

    // Cancelling the dialog keeps the color as it was.
    private string Pick(string current) => ColorChooser.Choose(current, this) ?? current;
}
