using System.Globalization;

namespace FrameHeat.Core.Metrics;

/// <summary>How the widget writes numbers: whole, rounded half away from zero, and the same in every culture.</summary>
public static class NumberText
{
    public static string Whole(double value)
        => Math.Round(value, MidpointRounding.AwayFromZero).ToString("0", CultureInfo.InvariantCulture);

    /// <summary>A figure with one decimal below ten and none from ten up, such as a speed, in the given culture.</summary>
    public static string Short(double value, CultureInfo culture)
    {
        var rounded = Math.Round(value, 1, MidpointRounding.AwayFromZero);
        return Math.Abs(rounded) >= 10 ? Whole(rounded) : rounded.ToString("0.0", culture);
    }
}
