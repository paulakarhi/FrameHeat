using System.Globalization;
using FrameHeat.Core.Settings;

namespace FrameHeat.Core.Metrics;

/// <summary>One unit a speed can be shown in, such as Mbit/s, and how many bytes per second make one of it.</summary>
/// <param name="WholeNumbers">True for the smallest unit, where a decimal would say nothing.</param>
public sealed record SpeedStep(string Symbol, double BytesPerSecond, bool WholeNumbers)
{
    /// <summary>The speed as a figure in this unit; a speed that rounds to nothing is a plain 0.</summary>
    public string Figure(double bytesPerSecond, CultureInfo culture)
    {
        var value = bytesPerSecond / BytesPerSecond;
        if (WholeNumbers)
            return NumberText.Whole(value);

        return Math.Round(value, 1, MidpointRounding.AwayFromZero) == 0 ? "0" : NumberText.Short(value, culture);
    }
}

/// <summary>Picks the unit a network speed is shown in, so a quiet connection reads 40 kbit/s and not 0.04 Mbit/s.</summary>
public static class Speed
{
    private const double BitsPerByte = 8;

    private static readonly SpeedStep[] BitSteps =
        [new("kbit/s", 125, WholeNumbers: true), new("Mbit/s", 125_000, WholeNumbers: false), new("Gbit/s", 125_000_000, WholeNumbers: false)];

    private static readonly SpeedStep[] ByteSteps =
        [new("KB/s", 1_000, WholeNumbers: true), new("MB/s", 1_000_000, WholeNumbers: false), new("GB/s", 1_000_000_000, WholeNumbers: false)];

    public static double BitsPerSecond(double bytesPerSecond) => bytesPerSecond * BitsPerByte;

    /// <summary>The largest unit in which the speed still reads as one or more, in bits or in bytes as chosen.</summary>
    public static SpeedStep StepFor(double bytesPerSecond, SpeedUnit unit)
    {
        var steps = unit == SpeedUnit.MegabytesPerSecond ? ByteSteps : BitSteps;
        return steps.LastOrDefault(step => Math.Round(bytesPerSecond / step.BytesPerSecond, 1, MidpointRounding.AwayFromZero) >= 1)
               ?? steps[0];
    }
}
