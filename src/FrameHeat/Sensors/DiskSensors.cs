using LibreHardwareMonitor.Hardware;

namespace FrameHeat.Sensors;

/// <summary>One tile per drive (at most four): temperature, how full it is, and how busy it is.</summary>
internal static class DiskSensors
{
    private const int MaxDisks = 4;

    // NVMe drives report the real temperature as "Composite Temperature"; SATA drives as "Temperature".
    private static readonly string[] TemperaturePriority = ["Composite Temperature", "Temperature"];

    public static IReadOnlyList<MetricReading> Read(IReadOnlyList<IHardware> hardware)
    {
        var disks = ChooseDisks(hardware);
        var readings = new List<MetricReading>(disks.Count);

        for (var i = 0; i < disks.Count; i++)
        {
            var disk = disks[i];

            var temperatures = disk.Temperatures().ToList();
            var temperature = temperatures.FirstValid(TemperaturePriority) ?? temperatures.Hottest();

            // The drive reports the temperature at which it starts throttling.
            var limit = disk.SensorsOf(SensorType.Temperature).Named("Warning Temperature").ValidTemperature();

            var loads = disk.SensorsOf(SensorType.Load).ToList();
            var usedSpace = loads.Named("Used Space").ValidPercent();
            var activity = loads.Named("Total Activity").ValidPercent();

            var ordinal = disks.Count == 1 ? 0 : i + 1;
            readings.Add(new MetricReading($"disk:{disk.Identifier}", MetricKind.Disk, disk.Name, temperature, usedSpace, activity, limit, ordinal));
        }

        return readings;
    }

    // USB sticks and similar also show up but have no temperature sensor. Prefer drives that do,
    // and fall back to any drive that reports used space.
    private static List<IHardware> ChooseDisks(IReadOnlyList<IHardware> hardware)
    {
        var storage = hardware.Where(device => device.HardwareType == HardwareType.Storage).ToList();
        var withTemperature = storage.Where(device => device.Temperatures().Any()).ToList();

        var chosen = withTemperature.Count > 0
            ? withTemperature
            : storage.Where(device => device.SensorsOf(SensorType.Load).Named("Used Space") is not null).ToList();

        return chosen.Take(MaxDisks).ToList();
    }
}
