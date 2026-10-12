using System.Collections.Concurrent;
using System.Windows.Media;

namespace FrameHeat.ViewModels;

/// <summary>Brushes that depend on user settings or are shared between tiles. Static colors live in Themes/Colors.xaml.</summary>
internal static class Palette
{
    // Colors that do not change with the settings: the value of a healthy device and of one without a reading.
    public static readonly Brush Text = Solid("#F3F6FB");
    public static readonly Brush Unknown = Solid("#6B7889");

    // A light fade from top to bottom gives panels and tiles depth.
    private static readonly Brush TileOnPanel = Gradient(Color.FromArgb(0x1C, 255, 255, 255), Color.FromArgb(0x08, 255, 255, 255));
    private static readonly Brush PanelEdge = Gradient(Color.FromArgb(0x3A, 255, 255, 255), Color.FromArgb(0x0E, 255, 255, 255));
    private static readonly Brush SolidPanel = Gradient(Color.FromRgb(0x1B, 0x22, 0x2E), Color.FromRgb(0x0B, 0x0E, 0x15));

    // Brushes that depend on a setting are created once per distinct value and then reused, so a
    // tile update does not allocate new brushes every second.
    private static readonly ConcurrentDictionary<string, Brush> UserBrushes = new();
    private static readonly ConcurrentDictionary<(string Warning, string Critical), SeverityColors> Severities = new();
    private static readonly ConcurrentDictionary<byte, Brush> TileBrushes = new();

    /// <summary>The alert colors the user has chosen.</summary>
    public static SeverityColors AlertColors(AppearanceSettings appearance)
        => Severities.GetOrAdd((appearance.WarningColor, appearance.CriticalColor), key => new SeverityColors(
            ColorOf(key.Warning, AppearanceSettings.DefaultWarningColor),
            ColorOf(key.Critical, AppearanceSettings.DefaultCriticalColor)));

    /// <summary>Without a panel each tile draws its own dark background, controlled by the opacity setting.</summary>
    public static Brush TileBackground(PanelStyle style, double opacity)
        => style == PanelStyle.None
            ? TileBrushes.GetOrAdd(ToAlpha(opacity), alpha => Solid(Color.FromArgb(alpha, 0x09, 0x0C, 0x12)))
            : TileOnPanel;

    /// <summary>Background of the whole widget: none, a translucent glass gradient or a solid one.</summary>
    public static Brush PanelBackground(PanelStyle style, double opacity) => style switch
    {
        PanelStyle.Solid => SolidPanel,
        PanelStyle.Glass => Gradient(
            Color.FromArgb(ToAlpha(opacity), 0x1B, 0x22, 0x2E),
            Color.FromArgb(ToAlpha(opacity), 0x0B, 0x0E, 0x15)),
        _ => Brushes.Transparent,
    };

    public static Brush PanelOutline(PanelStyle style) => style == PanelStyle.None ? Brushes.Transparent : PanelEdge;

    /// <summary>The user's accent color; an invalid value falls back to the default one.</summary>
    public static Brush Accent(string? color) => UserColor(color, AppearanceSettings.DefaultAccentColor);

    /// <summary>A solid brush for a color the user chose, or for the fallback when the color is not valid.</summary>
    public static Brush UserColor(string? color, string fallback)
        => UserBrushes.GetOrAdd(AppearanceSettings.IsValidColor(color) ? color! : fallback, hex => Solid(ColorOf(hex, fallback)));

    // 0..1 opacity as the 0..255 alpha channel.
    internal static byte ToAlpha(double opacity) => (byte)Math.Round(Math.Clamp(opacity, 0, 1) * 255);

    internal static Brush Solid(Color color)
    {
        var brush = new SolidColorBrush(color);
        brush.Freeze();
        return brush;
    }

    private static Brush Solid(string hex) => Solid(ColorOf(hex, hex));

    private static Color ColorOf(string hex, string fallback)
        => (Color)ColorConverter.ConvertFromString(AppearanceSettings.IsValidColor(hex) ? hex : fallback);

    private static Brush Gradient(Color top, Color bottom)
    {
        var brush = new LinearGradientBrush(top, bottom, 90);
        brush.Freeze();
        return brush;
    }
}
