namespace FrameHeat.Core.Alerts;

/// <summary>
/// The yellow (warning) and red (critical) thresholds for one metric. Critical is always at
/// least one degree above warning.
/// </summary>
public sealed class AlertLimits : ObservableObject
{
    // Accepted range for the two limits. Critical starts one above Warning's minimum so the two can never meet.
    private const double MinWarning = 20;
    private const double MaxWarning = 120;
    private const double MinCritical = 21;
    private const double MaxCritical = 125;

    private double _warning;
    private double _critical;

    public AlertLimits() : this(80, 90)
    {
    }

    public AlertLimits(double warning, double critical)
    {
        _warning = Math.Clamp(warning, MinWarning, MaxWarning);
        _critical = Math.Max(Math.Clamp(critical, MinCritical, MaxCritical), _warning + 1);
    }

    // The limits push each other: raising Warning past Critical lifts Critical, and lowering Critical
    // past Warning drags Warning down, so Critical always stays at least one degree above.
    public double Warning
    {
        get => _warning;
        set
        {
            if (Set(ref _warning, Math.Clamp(value, MinWarning, MaxWarning)) && _critical <= _warning)
                Critical = _warning + 1;
        }
    }

    public double Critical
    {
        get => _critical;
        set
        {
            if (Set(ref _critical, Math.Clamp(value, MinCritical, MaxCritical)) && _warning >= _critical)
                Warning = _critical - 1;
        }
    }
}
