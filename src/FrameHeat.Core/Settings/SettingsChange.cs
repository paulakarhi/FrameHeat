namespace FrameHeat.Core.Settings;

/// <summary>One change to the settings: the part it belongs to and the name of the property that changed.</summary>
public sealed record SettingsChange(SettingsArea Area, string? Property);
