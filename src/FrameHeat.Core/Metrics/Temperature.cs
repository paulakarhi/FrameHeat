using FrameHeat.Core.Settings;

namespace FrameHeat.Core.Metrics;

/// <summary>Turns the Celsius values the sensors report into the unit the user has chosen.</summary>
public static class Temperature
{
    public static double Convert(double celsius, TemperatureUnit unit)
        => unit == TemperatureUnit.Fahrenheit ? celsius * 9 / 5 + 32 : celsius;

    public static string Symbol(TemperatureUnit unit) => unit == TemperatureUnit.Fahrenheit ? "°F" : "°C";

    /// <summary>A Celsius value written in the chosen unit with its symbol, for example "86 °F".</summary>
    public static string Format(double celsius, TemperatureUnit unit)
        => $"{NumberText.Whole(Convert(celsius, unit))} {Symbol(unit)}";
}
