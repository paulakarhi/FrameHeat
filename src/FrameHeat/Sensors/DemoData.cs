namespace FrameHeat.Sensors;

/// <summary>
/// Synthetic readings that sweep from cold to overheating and back, to preview the alert colors
/// without heating up the machine.
/// </summary>
internal static class DemoData
{
    public const double CycleSeconds = 36;
    private const double LoadPeriodSeconds = 11;
    private const double FrameRatePeriodSeconds = 14;
    private const double DefaultRefreshRateHz = 144;
    private const double BytesPerMegabit = 1_000_000 / 8.0;
    private const double FastestDownloadMegabits = 300;

    /// <summary>
    /// The fake readings for a moment in the demo; every device is at a different point of its cycle.
    /// <paramref name="refreshRateHz"/> is the monitor's real refresh rate when it is known.
    /// </summary>
    public static SensorSnapshot At(double seconds, double? refreshRateHz = null)
    {
        var metrics = new List<MetricReading>
        {
            new("cpu", MetricKind.Cpu, "Demo", Heat(seconds, 0.00, 42, 100), Load(seconds, 0.0, 20, 95)),
            new("gpu", MetricKind.Gpu, "Demo", Heat(seconds, 0.12, 40, 92), Load(seconds, 0.5, 30, 99)),
            new("vram", MetricKind.Vram, "Demo", Heat(seconds, 0.18, 38, 98), 93),
            new("memory", MetricKind.Memory, "Demo", Heat(seconds, 0.25, 34, 82), Load(seconds, 0.2, 45, 97)),
            new("disk:1", MetricKind.Disk, "Demo", Heat(seconds, 0.38, 36, 78), 42, Load(seconds, 0.7, 0, 60), Ordinal: 1),
            new("disk:2", MetricKind.Disk, "Demo", Heat(seconds, 0.50, 34, 76), 88, Load(seconds, 0.3, 0, 40), Ordinal: 2),
            new("fps", MetricKind.Fps, "Demo", null, null, Frames: Frames(seconds, refreshRateHz ?? DefaultRefreshRateHz)),
            new("network", MetricKind.Network, "Demo", null, null, Network: Network(seconds)),
            new("ping", MetricKind.Ping, "Demo", null, null, Ping: Ping(seconds)),
        };

        return new SensorSnapshot(DateTimeOffset.Now, metrics);
    }

    // A frame rate that rises and falls between 45 and 145, with a one percent low well below it.
    private static FrameStats Frames(double seconds, double refreshRateHz)
    {
        var fps = 95 + 50 * Math.Sin(2 * Math.PI * seconds / FrameRatePeriodSeconds);
        return new FrameStats(fps, 1000 / fps, fps * 0.7, refreshRateHz);
    }

    // A download that swings between 2 and 300 Mbit/s over a gigabit connection, with a much smaller upload.
    private static NetworkStats Network(double seconds) => new(
        Load(seconds, 0.4, 2, FastestDownloadMegabits) * BytesPerMegabit,
        Load(seconds, 0.9, 1, 40) * BytesPerMegabit,
        1_000_000_000,
        FastestDownloadMegabits * BytesPerMegabit);

    // A delay between 8 and 90 ms, now and then with one message lost.
    private static PingStats Ping(double seconds)
    {
        var delay = Load(seconds, 0.1, 8, 90);
        return new PingStats(delay, delay * 0.95, seconds % 20 < 1 ? 10 : 0);
    }

    // Triangle wave from low to high and back; each device runs at its own phase.
    private static double Heat(double seconds, double phase, double low, double high)
    {
        var position = (seconds / CycleSeconds + phase) % 1.0;
        var wave = position < 0.5 ? position * 2 : (1 - position) * 2;
        return low + (high - low) * wave;
    }

    private static double Load(double seconds, double phase, double low, double high)
    {
        var wave = 0.5 + 0.5 * Math.Sin(2 * Math.PI * (seconds / LoadPeriodSeconds + phase));
        return low + (high - low) * wave;
    }
}
