namespace FrameHeat.Core.Metrics;

/// <summary>Remembers the latest ping results. Results arrive on one thread while the figures are read on another.</summary>
public sealed class PingTracker(int window = 10)
{
    private readonly object _gate = new();
    private readonly Queue<double?> _results = new();

    /// <summary>Records one message: the round trip in milliseconds, or null when it got no reply.</summary>
    public void Add(double? roundTripMs)
    {
        lock (_gate)
        {
            _results.Enqueue(roundTripMs);
            while (_results.Count > window)
                _results.Dequeue();
        }
    }

    /// <summary>The figures over the latest messages; null before the first message has been answered or has timed out.</summary>
    public PingStats? Read()
    {
        double?[] results;
        lock (_gate)
            results = [.. _results];

        if (results.Length == 0)
            return null;

        var replies = results.OfType<double>().ToList();
        return new PingStats(
            results[^1],
            replies.Count > 0 ? replies.Average() : null,
            100.0 * (results.Length - replies.Count) / results.Length);
    }
}
