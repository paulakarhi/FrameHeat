using System.Windows.Media;

namespace FrameHeat.ViewModels;

/// <summary>
/// The brushes that show how hot a device is: its value, ring, tile border and tile tint. The warning
/// and critical colors are the user's, so the set is built for one pair of colors and reused.
/// </summary>
internal sealed class SeverityColors
{
    private static readonly Brush Neutral = Palette.Solid(Color.FromArgb(0x1A, 255, 255, 255));

    // The same color is used for value, border and tint, only more or less see-through.
    private const byte WarningTintAlpha = 0x33;
    private const byte CriticalTintAlpha = 0x4D;
    private const byte WarningOutlineAlpha = 0x99;
    private const byte CriticalOutlineAlpha = 0xE6;

    private readonly Brush _warning;
    private readonly Brush _critical;
    private readonly Brush _warningTint;
    private readonly Brush _criticalTint;
    private readonly Brush _warningOutline;
    private readonly Brush _criticalOutline;

    public SeverityColors(Color warning, Color critical)
    {
        _warning = Palette.Solid(WithAlpha(warning, 255));
        _critical = Palette.Solid(WithAlpha(critical, 255));
        _warningTint = Palette.Solid(WithAlpha(warning, WarningTintAlpha));
        _criticalTint = Palette.Solid(WithAlpha(critical, CriticalTintAlpha));
        _warningOutline = Palette.Solid(WithAlpha(warning, WarningOutlineAlpha));
        _criticalOutline = Palette.Solid(WithAlpha(critical, CriticalOutlineAlpha));
    }

    /// <summary>Color of a value: white when fine, the warning or critical color when hot, grey without a reading.</summary>
    public Brush ForSeverity(Severity severity) => severity switch
    {
        Severity.Critical => _critical,
        Severity.Warning => _warning,
        Severity.Unknown => Palette.Unknown,
        _ => Palette.Text,
    };

    /// <summary>Gauge ring color: the user's accent when all is well, otherwise the alert color.</summary>
    public Brush Gauge(Severity severity, Brush accent) => severity == Severity.Normal ? accent : ForSeverity(severity);

    /// <summary>Faint fill behind a tile that is warm or too hot.</summary>
    public Brush Tint(Severity severity) => severity switch
    {
        Severity.Critical => _criticalTint,
        Severity.Warning => _warningTint,
        _ => Brushes.Transparent,
    };

    /// <summary>Tile border; strongest when critical.</summary>
    public Brush Outline(Severity severity) => severity switch
    {
        Severity.Critical => _criticalOutline,
        Severity.Warning => _warningOutline,
        _ => Neutral,
    };

    // The user may have picked a color with transparency of its own; the alert brushes decide that themselves.
    private static Color WithAlpha(Color color, byte alpha) => Color.FromArgb(alpha, color.R, color.G, color.B);
}
