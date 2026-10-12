using System.Windows;

namespace FrameHeat.ViewModels;

/// <summary>The corners and gaps of the panel and its tiles, worked out from the roundness and spacing settings.</summary>
internal sealed record TileShape(CornerRadius Tile, CornerRadius Inner, CornerRadius Panel, Thickness Margin)
{
    // The panel's corners are a little rounder than the tiles inside it, so the two curves look parallel.
    // Inner is the corner of the tint inside a tile, one pixel less because of the tile's border.
    private const double PanelRadiusExtra = 4;

    public static TileShape From(AppearanceSettings appearance) => new(
        Tile: new CornerRadius(appearance.CornerRadius),
        Inner: new CornerRadius(Math.Max(0, appearance.CornerRadius - 1)),
        Panel: new CornerRadius(appearance.CornerRadius + PanelRadiusExtra),
        Margin: new Thickness(appearance.TileSpacing / 2));
}
