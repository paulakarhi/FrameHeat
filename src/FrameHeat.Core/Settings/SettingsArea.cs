namespace FrameHeat.Core.Settings;

/// <summary>The part of the settings a change belongs to; <see cref="SettingsChange"/> carries it so listeners can ignore changes they do not care about.</summary>
public enum SettingsArea
{
    General,
    Appearance,
    Tiles,
    Alerts,
    Placement,
}
