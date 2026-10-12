namespace FrameHeat.Core.Metrics;

/// <summary>Works out frame rate, frame time and the one percent low from the moments a game presented its frames.</summary>
public static class FrameAnalyzer
{
    /// <summary>How far back the frame rate and frame time look.</summary>
    public const double RateWindowSeconds = 1;

    /// <summary>How far back the one percent low looks. A longer span is needed to catch rare stutters.</summary>
    public const double LowWindowSeconds = 10;

    // Below this many frames the slowest one percent is a single unlucky frame rather than a trend.
    private const int MinFramesForLow = 30;

    /// <param name="presentTimes">Moments in seconds, oldest first.</param>
    /// <param name="now">The end of the window, on the same clock as <paramref name="presentTimes"/>.</param>
    /// <returns>Null when the game has not presented enough frames in the last second.</returns>
    public static FrameStats? Analyze(IReadOnlyList<double> presentTimes, double now, double? refreshRateHz)
    {
        var recent = Within(presentTimes, now, RateWindowSeconds);
        if (recent.Count < 2)
            return null;

        var span = recent[^1] - recent[0];
        if (span <= 0)
            return null;

        var fps = (recent.Count - 1) / span;
        return new FrameStats(fps, 1000 / fps, OnePercentLow(Within(presentTimes, now, LowWindowSeconds)), refreshRateHz);
    }

    private static List<double> Within(IReadOnlyList<double> times, double now, double seconds)
        => times.Where(time => time > now - seconds && time <= now).ToList();

    private static double? OnePercentLow(List<double> times)
    {
        var intervals = times.Zip(times.Skip(1), (earlier, later) => later - earlier).Where(gap => gap > 0).ToList();
        if (intervals.Count < MinFramesForLow)
            return null;

        var slowest = intervals.OrderByDescending(gap => gap).Take(Math.Max(1, intervals.Count / 100));
        return 1 / slowest.Average();
    }
}
