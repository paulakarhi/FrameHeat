namespace FrameHeat.Core.Metrics;

/// <summary>
/// Remembers when each process presented a frame, so its frame rate can be read at any time. Frames
/// arrive on one thread while the readings are taken on another.
/// </summary>
public sealed class FrameTracker
{
    // Slightly more than the longest window the analyzer uses, so nothing it needs is dropped.
    private const double KeepSeconds = FrameAnalyzer.LowWindowSeconds + 2;

    private readonly object _gate = new();
    private readonly Dictionary<int, List<double>> _presents = new();

    public void OnPresent(int processId, double seconds)
    {
        lock (_gate)
        {
            if (!_presents.TryGetValue(processId, out var times))
                _presents[processId] = times = [];

            times.Add(seconds);
            times.RemoveAll(time => time < seconds - KeepSeconds);
        }
    }

    public FrameStats? Read(int processId, double now, double? refreshRateHz)
    {
        double[] times;
        lock (_gate)
        {
            if (!_presents.TryGetValue(processId, out var found))
                return null;

            times = [.. found];
        }

        return FrameAnalyzer.Analyze(times, now, refreshRateHz);
    }

    /// <summary>Forgets processes that have not presented anything for a while, such as closed games.</summary>
    public void Prune(double now)
    {
        lock (_gate)
        {
            foreach (var processId in _presents.Where(entry => entry.Value.Count == 0 || entry.Value[^1] < now - KeepSeconds)
                                               .Select(entry => entry.Key).ToList())
                _presents.Remove(processId);
        }
    }
}
