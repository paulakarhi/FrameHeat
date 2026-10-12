namespace FrameHeat.Core.Settings;

public enum TileDesign
{
    /// <summary>Ring that fills as the temperature approaches the critical limit.</summary>
    Gauge,

    /// <summary>Large number with a usage bar; takes less space.</summary>
    Compact,

    /// <summary>One line of text per device: name, value and unit. Takes the least space.</summary>
    TextRow,
}
