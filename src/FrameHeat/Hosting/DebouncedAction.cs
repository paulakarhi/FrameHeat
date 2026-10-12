using System.Windows.Threading;

namespace FrameHeat.Hosting;

/// <summary>Runs an action once a burst of requests has settled.</summary>
internal sealed class DebouncedAction
{
    private readonly DispatcherTimer _timer;

    public DebouncedAction(Dispatcher dispatcher, TimeSpan delay, Action action)
    {
        _timer = new DispatcherTimer(DispatcherPriority.Background, dispatcher) { Interval = delay };
        _timer.Tick += (_, _) =>
        {
            _timer.Stop();
            action();
        };
    }

    /// <summary>(Re)starts the countdown; the action runs once, when no further request arrives within the delay.</summary>
    public void Request()
    {
        _timer.Stop();
        _timer.Start();
    }

    /// <summary>Drops a pending run, for example because the caller is about to do the work itself.</summary>
    public void Cancel() => _timer.Stop();
}
