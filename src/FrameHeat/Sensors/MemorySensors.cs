using LibreHardwareMonitor.Hardware;

namespace FrameHeat.Sensors;

/// <summary>The RAM tile: usage of physical memory and, where the modules have a sensor, their temperature.</summary>
internal static class MemorySensors
{
    private const string PhysicalMemory = "Total Memory";
    private const string VirtualMemory = "Virtual Memory";
    private const string ModuleHighLimit = "Thermal Sensor High Limit";

    public static MetricReading? Read(IReadOnlyList<IHardware> hardware)
    {
        var memories = hardware.Where(device => device.HardwareType == HardwareType.Memory).ToList();
        if (memories.Count == 0)
            return null;

        // Virtual memory includes the page file and always reads higher than physical memory.
        var physical = memories.FirstOrDefault(device => device.Name == PhysicalMemory)
                       ?? memories.FirstOrDefault(device => device.Name != VirtualMemory);
        var usage = physical?.SensorsOf(SensorType.Load).Named("Memory").ValidPercent();

        // Only some machines expose module temperatures; the hottest module counts.
        var temperature = memories.SelectMany(device => device.Temperatures()).Hottest();

        // Modules report their own high limit; with several, the lowest applies.
        var limit = memories
            .SelectMany(device => device.SensorsOf(SensorType.Temperature))
            .Where(sensor => sensor.Name == ModuleHighLimit)
            .Select(sensor => sensor.ValidTemperature())
            .Min();

        var deviceName = memories
            .FirstOrDefault(device => device.Name is not (PhysicalMemory or VirtualMemory))?.Name ?? "RAM";

        return new MetricReading("memory", MetricKind.Memory, deviceName, temperature, usage, DeviceLimitC: limit);
    }
}
