using System.Windows;
using FrameHeat.Hosting;
using FrameHeat.Sensors;
using FrameHeat.Services;
using FrameHeat.Startup;

namespace FrameHeat;

/// <summary>Application entry point: decides what this launch does, then hands over to <see cref="AppController"/>.</summary>
public partial class App : Application
{
    // Held for the whole run so a second launch can tell that the program is already running.
    private Mutex? _singleInstance;
    private bool _ownsSingleInstance;
    private AppController? _controller;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        RegisterErrorLogging();

        var options = CommandLineOptions.Parse(e.Args);

        // The language is fixed for the whole run, so it is chosen before any window is created.
        Loc.Use(LanguageConfig.Read(options.Language));

        // --dump: write the hardware report and exit without opening anything.
        if (options.DumpPath is not null)
        {
            WriteHardwareReport(options.DumpPath);
            Shutdown();
            return;
        }

        // A second copy would show a second widget and fight over the hotkeys.
        if (!options.AllowMultipleInstances && !TryClaimSingleInstance())
        {
            Shutdown();
            return;
        }

        _controller = new AppController(options);
        _controller.Start();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _controller?.Dispose();
        ReleaseSingleInstance();
        base.OnExit(e);
    }

    private bool TryClaimSingleInstance()
    {
        _singleInstance = new Mutex(initiallyOwned: true, AppIdentity.SingleInstanceMutex, out _ownsSingleInstance);
        return _ownsSingleInstance;
    }

    private void ReleaseSingleInstance()
    {
        if (_ownsSingleInstance)
            _singleInstance?.ReleaseMutex();

        _singleInstance?.Dispose();
    }

    // Unexpected errors are logged. Errors on the UI thread and in background tasks are then ignored,
    // so one bad reading does not close the widget in the middle of a game.
    private void RegisterErrorLogging()
    {
        DispatcherUnhandledException += (_, args) =>
        {
            Log.Error("Unhandled UI exception", args.Exception);
            args.Handled = true;
        };

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            Log.Error("Unhandled exception", args.ExceptionObject as Exception);

        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            Log.Error("Unobserved task exception", args.Exception);
            args.SetObserved();
        };
    }

    // Report mode: open the sensors, sample long enough for load readings to settle, write the file.
    private static void WriteHardwareReport(string path)
    {
        using var monitor = new HardwareMonitor();
        monitor.SampleForReport();
        File.WriteAllText(path, monitor.BuildReport());
    }
}
