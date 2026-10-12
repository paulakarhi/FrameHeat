using LibreHardwareMonitor.Hardware;

namespace FrameHeat.Sensors;

/// <summary>Builds the list of tiles from the sensor library's hardware tree.</summary>
internal static class SensorPicker
{
    /// <summary>One reading per tile, in display order: CPU, GPU, video memory, RAM, then the drives.</summary>
    public static IReadOnlyList<MetricReading> Pick(IEnumerable<IHardware> hardware)
    {
        var devices = hardware.ToList();
        var readings = new List<MetricReading>();

        AddIfPresent(readings, CpuSensors.Read(devices));
        AddIfPresent(readings, GpuSensors.ReadCore(devices));
        AddIfPresent(readings, GpuSensors.ReadVram(devices));
        AddIfPresent(readings, MemorySensors.Read(devices));
        readings.AddRange(DiskSensors.Read(devices));

        return readings;
    }

    private static void AddIfPresent(List<MetricReading> readings, MetricReading? reading)
    {
        if (reading is not null)
            readings.Add(reading);
    }
}
