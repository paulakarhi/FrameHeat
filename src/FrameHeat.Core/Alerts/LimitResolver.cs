namespace FrameHeat.Core.Alerts;

/// <summary>Chooses which limits apply to a device: its own, if it reports one, or the configured ones.</summary>
public static class LimitResolver
{
    /// <summary>How many degrees before the device's own limit the warning starts.</summary>
    public const double ApproachMarginC = 8;

    private const double MinPlausibleDeviceLimit = 40;
    private const double MaxPlausibleDeviceLimit = 110;

    /// <summary>
    /// Red starts at the limit the device reports for itself and yellow a few degrees earlier.
    /// Without a usable device limit the configured thresholds apply.
    /// </summary>
    public static (double Warning, double Critical) Resolve(AlertLimits configured, double? deviceLimit, bool useDeviceLimit)
    {
        if (useDeviceLimit && deviceLimit is >= MinPlausibleDeviceLimit and <= MaxPlausibleDeviceLimit)
            return (deviceLimit.Value - ApproachMarginC, deviceLimit.Value);

        return (configured.Warning, configured.Critical);
    }
}
