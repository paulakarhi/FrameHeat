using System.Diagnostics;
using System.Windows.Threading;
using FrameHeat.Sensors;

namespace FrameHeat.Hosting;

/// <summary>Feeds synthetic readings to the widget while demo mode is on.</summary>
internal sealed class DemoRunner
{
    private static readonly TimeSpan Interval = TimeSpan.FromMilliseconds(250);

    private readonly DispatcherTimer _timer;
    private readonly Stopwatch _clock = new();
    private readonly Func<double?>? _refreshRate;
    private volatile bool _running;
    private double? _currentRefreshRate;

    // Every tick publishes the synthetic readings for the current point of the demo cycle.
    // The monitor's refresh rate, when it can be read, makes the demo show the real one.
    public DemoRunner(Dispatcher dispatcher, Action<SensorSnapshot> publish, Func<double?>? refreshRate = null)
    {
        _refreshRate = refreshRate;
        _timer = new DispatcherTimer(DispatcherPriority.Normal, dispatcher) { Interval = Interval };
        _timer.Tick += (_, _) => publish(DemoData.At(_clock.Elapsed.TotalSeconds, _currentRefreshRate));
    }

    /// <summary>Safe to read from any thread; the sensor thread checks it before publishing.</summary>
    public bool IsRunning => _running;

    /// <summary>Starts the demo, or stops it when it is already running.</summary>
    public void Toggle()
    {
        if (_running)
            Stop();
        else
            Start();
    }

    public void Stop()
    {
        _running = false;
        _timer.Stop();
        _clock.Stop();
    }

    private void Start()
    {
        _currentRefreshRate = _refreshRate?.Invoke();
        _clock.Restart();
        _running = true;
        _timer.Start();
    }
}
