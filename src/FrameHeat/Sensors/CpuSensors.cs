using LibreHardwareMonitor.Hardware;

namespace FrameHeat.Sensors;

/// <summary>Picks the CPU temperature and load out of the sensor library's device tree.</summary>
internal static class CpuSensors
{
    // Intel reports a package temperature, AMD Tctl/Tdie; the hottest core is the fallback.
    private static readonly string[] TemperaturePriority =
        ["CPU Package", "Core (Tctl/Tdie)", "Core (Tdie)", "Core Max", "Core Average"];

    public static MetricReading? Read(IReadOnlyList<IHardware> hardware)
    {
        var cpu = hardware.FirstOrDefault(device => device.HardwareType == HardwareType.Cpu);
        if (cpu is null)
            return null;

        var temperatures = cpu.Temperatures().ToList();
        var temperature = temperatures.FirstValid(TemperaturePriority) ?? temperatures.Hottest();
        var load = cpu.SensorsOf(SensorType.Load).Named("CPU Total").ValidPercent();

        return new MetricReading("cpu", MetricKind.Cpu, cpu.Name, temperature, load);
    }
}
