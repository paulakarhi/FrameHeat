using System.Globalization;

namespace FrameHeat.Core.Metrics;

/// <summary>How the widget writes numbers: whole, rounded half away from zero, and the same in every culture.</summary>
public static class NumberText
{
    public static string Whole(double value)
        => Math.Round(value, MidpointRounding.AwayFromZero).ToString("0", CultureInfo.InvariantCulture);
}
