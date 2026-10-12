namespace FrameHeat.Core.Alerts;

/// <summary>
/// Turns a reading into a severity. The severity rises as soon as a limit is crossed but falls only
/// once the value is a few degrees below it, so a value hovering at a limit does not flicker.
/// </summary>
public sealed class SeverityTracker
{
    private readonly double _hysteresis;

    public SeverityTracker(double hysteresis = 2.0) => _hysteresis = hysteresis;

    public Severity Current { get; private set; } = Severity.Unknown;

    /// <summary>Judges a new value against the limits and returns the severity to show.</summary>
    public Severity Update(double? value, double warning, double critical)
    {
        if (value is not { } v)
            return Current = Severity.Unknown;

        // What the value alone says.
        var raw = v >= critical ? Severity.Critical
                : v >= warning ? Severity.Warning
                : Severity.Normal;

        // Getting worse (or the first reading) counts immediately.
        if (Current == Severity.Unknown || raw >= Current)
            return Current = raw;

        // Getting better: stay at the current level until the value is clearly below the limit it crossed.
        var heldLimit = Current == Severity.Critical ? critical : warning;
        if (v < heldLimit - _hysteresis)
            Current = raw;

        return Current;
    }
}
