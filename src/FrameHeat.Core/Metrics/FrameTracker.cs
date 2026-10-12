namespace FrameHeat.Core.Metrics;

/// <summary>
/// Remembers when each process presented a frame, so its frame rate can be read at any time. Frames
/// arrive on one thread while the readings are taken on another.
/// </summary>
/// <remarks>
/// Frames reach the listener seconds late, in bursts and out of order. The frame rate is therefore taken from
/// the newest frames delivered so far, not from a window that ends at the present moment, and a process counts
/// as drawing for as long as frames keep arriving.
/// </remarks>
public sealed class FrameTracker
{
    /// <summary>How long a process may go without a frame arriving before it counts as no longer drawing.</summary>
    public const double SilentSeconds = 5;

    // Slightly more than the longest window the analyzer uses, so nothing it needs is dropped.
    private const double KeepSeconds = FrameAnalyzer.LowWindowSeconds + 2;

    private readonly object _gate = new();
    private readonly Dictionary<int, ProcessFrames> _processes = new();

    /// <param name="frameSeconds">When the frame was presented, on the clock of the event.</param>
    /// <param name="arrivalSeconds">When the event reached the listener, on the clock the readings use.</param>
    public void OnPresent(int processId, double frameSeconds, double arrivalSeconds)
    {
        lock (_gate)
        {
            if (!_processes.TryGetValue(processId, out var frames))
                _processes[processId] = frames = new ProcessFrames();

            frames.Add(frameSeconds, arrivalSeconds);
        }
    }

    /// <param name="nowSeconds">The present moment on the clock of the arrivals.</param>
    public FrameStats? Read(int processId, double nowSeconds, double? refreshRateHz)
    {
        double[] times;
        double newest;
        lock (_gate)
        {
            if (!_processes.TryGetValue(processId, out var frames) || nowSeconds - frames.LastArrival > SilentSeconds)
                return null;

            times = frames.Times();
            newest = frames.Newest;
        }

        return FrameAnalyzer.Analyze(times, newest, refreshRateHz);
    }

    /// <summary>Forgets processes that have not presented anything for a while, such as closed games.</summary>
    public void Prune(double nowSeconds)
    {
        lock (_gate)
        {
            foreach (var processId in _processes.Where(entry => nowSeconds - entry.Value.LastArrival > KeepSeconds)
                                                .Select(entry => entry.Key).ToList())
                _processes.Remove(processId);
        }
    }

    private sealed class ProcessFrames
    {
        private readonly List<double> _times = [];

        public double Newest { get; private set; } = double.NegativeInfinity;

        public double LastArrival { get; private set; }

        public void Add(double frameSeconds, double arrivalSeconds)
        {
            _times.Add(frameSeconds);
            Newest = Math.Max(Newest, frameSeconds);
            LastArrival = arrivalSeconds;
            _times.RemoveAll(time => time < Newest - KeepSeconds);
        }

        public double[] Times() => [.. _times];
    }
}
