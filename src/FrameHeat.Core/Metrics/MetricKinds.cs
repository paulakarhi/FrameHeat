namespace FrameHeat.Core.Metrics;

/// <summary>Groups of <see cref="MetricKind"/> that several parts of the program need.</summary>
public static class MetricKinds
{
    /// <summary>The devices whose temperature is judged against alert limits.</summary>
    public static readonly IReadOnlyList<MetricKind> WithTemperature =
        [MetricKind.Cpu, MetricKind.Gpu, MetricKind.Vram, MetricKind.Memory, MetricKind.Disk];
}
