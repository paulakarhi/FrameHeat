namespace FrameHeat.Core.Alerts;

/// <summary>Ordered by badness: a higher value is worse.</summary>
public enum Severity
{
    /// <summary>There is no reading to judge.</summary>
    Unknown = 0,
    Normal = 1,
    Warning = 2,
    Critical = 3,
}
