using LibreHardwareMonitor.Hardware;

namespace FrameHeat.Sensors;

/// <summary>Picks the temperature and load of the graphics card, and of its video memory.</summary>
internal static class GpuSensors
{
    private static readonly string[] VramTemperaturePriority = ["GPU Memory Junction", "GPU Memory"];

    /// <summary>The GPU tile: core temperature and load. Load falls back to the busiest Direct3D engine.</summary>
    public static MetricReading? ReadCore(IReadOnlyList<IHardware> hardware)
    {
        var gpu = FindPrimary(hardware);
        if (gpu is null)
            return null;

        var temperatures = gpu.Temperatures().ToList();
        var temperature = temperatures.FirstValid("GPU Core") ?? temperatures.Hottest();

        var loads = gpu.SensorsOf(SensorType.Load).ToList();
        var load = loads.Named("GPU Core").ValidPercent()
                   ?? loads.Where(sensor => sensor.Name.StartsWith("D3D", StringComparison.Ordinal))
                       .Select(sensor => sensor.ValidPercent())
                       .Max();

        return new MetricReading("gpu", MetricKind.Gpu, gpu.Name, temperature, load);
    }

    /// <summary>
    /// Video memory temperature and fill level. Only discrete cards have it, and only some expose
    /// a memory temperature.
    /// </summary>
    public static MetricReading? ReadVram(IReadOnlyList<IHardware> hardware)
    {
        var gpu = FindPrimary(hardware);
        if (gpu is null or { HardwareType: HardwareType.GpuIntel })
            return null;

        var temperatures = gpu.Temperatures()
            .Where(sensor => sensor.Name.Contains("Memory", StringComparison.OrdinalIgnoreCase))
            .ToList();
        var temperature = temperatures.FirstValid(VramTemperaturePriority) ?? temperatures.Hottest();
        var usage = gpu.SensorsOf(SensorType.Load).Named("GPU Memory").ValidPercent();

        if (temperature is null && usage is null)
            return null;

        return new MetricReading("vram", MetricKind.Vram, gpu.Name, temperature, usage);
    }

    /// <summary>A discrete GPU wins over the integrated one, since games run on it.</summary>
    internal static IHardware? FindPrimary(IReadOnlyList<IHardware> hardware)
        => hardware.FirstOrDefault(device => device.HardwareType is HardwareType.GpuNvidia or HardwareType.GpuAmd)
           ?? hardware.FirstOrDefault(device => device.HardwareType == HardwareType.GpuIntel);
}
