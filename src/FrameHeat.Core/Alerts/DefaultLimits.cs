using FrameHeat.Core.Metrics;
using FrameHeat.Core.Settings;

namespace FrameHeat.Core.Alerts;

/// <summary>The built-in warning and critical limits, used until the user changes them.</summary>
public static class DefaultLimits
{
    // Usage alerts for memory and for how full a disk is: yellow at 85 %, red at 95 %.
    private static readonly (double Warning, double Critical) NearlyFull = (85, 95);

    /// <summary>Temperature limits in °C. CPUs tolerate more heat than memory modules and drives.</summary>
    public static AlertLimits ForTemperature(MetricKind kind) => kind switch
    {
        MetricKind.Cpu => new AlertLimits(80, 90),
        MetricKind.Gpu => new AlertLimits(78, 85),
        MetricKind.Vram => new AlertLimits(85, 95),
        MetricKind.Memory => new AlertLimits(60, 75),
        MetricKind.Disk => new AlertLimits(60, 70),
        _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, "This kind has no temperature limits."),
    };

    /// <summary>
    /// Usage alerts apply only to memory and to disk fullness. Sustained 100 % load on a CPU or GPU
    /// is normal in games, so those never alert on load.
    /// </summary>
    public static (double Warning, double Critical)? ForUsage(MetricKind kind, DiskUsageMode diskMode) => kind switch
    {
        MetricKind.Memory => NearlyFull,
        MetricKind.Disk when diskMode == DiskUsageMode.UsedSpace => NearlyFull,
        _ => null,
    };
}
