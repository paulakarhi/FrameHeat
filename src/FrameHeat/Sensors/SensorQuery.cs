using LibreHardwareMonitor.Hardware;

namespace FrameHeat.Sensors;

/// <summary>Helpers for picking readings out of the sensor library's hardware tree.</summary>
internal static class SensorQuery
{
    private const double MaxPlausibleTemperatureC = 130;

    // The library lists device limits and measurement resolution as temperature sensors too
    // ("Critical Temperature", "Thermal Sensor High Limit"). They are not measurements.
    private static readonly string[] NonMeasurementWords =
        ["Limit", "Resolution", "Warning", "Critical", "Distance", "Threshold", "TjMax"];

    public static IEnumerable<ISensor> SensorsOf(this IHardware hardware, SensorType type)
        => hardware.Sensors.Where(sensor => sensor.SensorType == type)
            .Concat(hardware.SubHardware.SelectMany(sub => sub.SensorsOf(type)));

    /// <summary>Temperature sensors that are actual measurements.</summary>
    public static IEnumerable<ISensor> Temperatures(this IHardware hardware)
        => hardware.SensorsOf(SensorType.Temperature)
            .Where(sensor => !NonMeasurementWords.Any(word => sensor.Name.Contains(word, StringComparison.OrdinalIgnoreCase)));

    public static ISensor? Named(this IEnumerable<ISensor> sensors, string name)
        => sensors.FirstOrDefault(sensor => sensor.Name == name);

    /// <summary>Null for missing readings and for the zero or absurd values sensors sometimes report.</summary>
    public static double? ValidTemperature(this ISensor? sensor)
        => sensor?.Value is float value and > 0 and < (float)MaxPlausibleTemperatureC ? value : null;

    /// <summary>A load as a percentage between 0 and 100; null when the sensor has no usable value.</summary>
    public static double? ValidPercent(this ISensor? sensor)
        => sensor?.Value is float value && float.IsFinite(value) ? Math.Clamp(value, 0, 100) : null;

    /// <summary>The first valid temperature among the given sensor names, tried in order.</summary>
    public static double? FirstValid(this IEnumerable<ISensor> sensors, params string[] names)
    {
        var list = sensors as IReadOnlyCollection<ISensor> ?? sensors.ToList();
        foreach (var name in names)
        {
            if (list.Named(name).ValidTemperature() is { } value)
                return value;
        }

        return null;
    }

    public static double? Hottest(this IEnumerable<ISensor> sensors)
        => sensors.Select(sensor => sensor.ValidTemperature()).Max();
}
