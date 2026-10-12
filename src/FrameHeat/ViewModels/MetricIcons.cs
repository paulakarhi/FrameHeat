namespace FrameHeat.ViewModels;

/// <summary>Outline icons on a 24 x 24 grid. They are stroked, not filled, so they follow the label color.</summary>
internal static class MetricIcons
{
    private const string Cpu =
        "M7,7 H17 V17 H7 Z M9.5,3 V7 M14.5,3 V7 M9.5,17 V21 M14.5,17 V21 M3,9.5 H7 M3,14.5 H7 M17,9.5 H21 M17,14.5 H21";

    private const string Gpu =
        "M2,6 H22 V16 H2 Z M10.5,11 A3,3 0 1 1 4.5,11 A3,3 0 1 1 10.5,11 M14,9 H19 M14,12.5 H19 M5,16 V19 M9,16 V19";

    private const string Vram =
        "M3,5 H21 V19 H3 Z M6,8.5 H10 V12.5 H6 Z M14,8.5 H18 V12.5 H14 Z M6,16 H18";

    private const string Memory =
        "M2,7 H22 V15 H2 Z M5,15 V18 M8,15 V18 M11,15 V18 M14,15 V18 M17,15 V18 M5,10.5 H8.5 M10.5,10.5 H14 M16,10.5 H19";

    private const string Disk =
        "M6,4 H18 A2,2 0 0 1 20,6 V18 A2,2 0 0 1 18,20 H6 A2,2 0 0 1 4,18 V6 A2,2 0 0 1 6,4 Z M8,8 H16 M8,16 H12 M16,16 H16.1";

    // A speedometer: an arc with a needle.
    private const string Fps =
        "M3,17 A9,9 0 0 1 21,17 M12,17 L17,10 M12,17 H12.1";

    // A download arrow beside an upload arrow.
    private const string Network =
        "M8,4 V18 M4,14 L8,18 L12,14 M16,20 V6 M12,10 L16,6 L20,10";

    // A pulse line, like a heartbeat.
    private const string Ping =
        "M2,12 H7 L10,5 L14,19 L17,12 H22";

    /// <summary>The path data (WPF geometry mini-language) of the icon for a kind of device.</summary>
    public static string For(MetricKind kind) => kind switch
    {
        MetricKind.Cpu => Cpu,
        MetricKind.Gpu => Gpu,
        MetricKind.Vram => Vram,
        MetricKind.Memory => Memory,
        MetricKind.Fps => Fps,
        MetricKind.Network => Network,
        MetricKind.Ping => Ping,
        _ => Disk,
    };
}
