namespace FrameHeat.ViewModels;

/// <summary>Tile titles. Only the disk label is translated; the other names are the same everywhere.</summary>
internal static class MetricLabels
{
    public static string For(MetricReading reading) => reading.Kind switch
    {
        MetricKind.Cpu => "CPU",
        MetricKind.Gpu => "GPU",
        MetricKind.Vram => "VRAM",
        MetricKind.Memory => "RAM",
        MetricKind.Fps => "FPS",
        _ => reading.Ordinal > 0 ? $"{Loc.T("metric_disk")} {reading.Ordinal}" : Loc.T("metric_disk"),
    };
}
