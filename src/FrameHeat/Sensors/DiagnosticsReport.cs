using System.Globalization;
using System.Text;
using FrameHeat.Services;
using FrameHeat.ViewModels;
using LibreHardwareMonitor.Hardware;

namespace FrameHeat.Sensors;

/// <summary>
/// Plain-text listing of every device and sensor found, with the system state that explains
/// missing temperatures (permissions, driver).
/// </summary>
internal static class DiagnosticsReport
{
    // Layout of the report: system state first, then the sensors the widget actually uses,
    // then everything the sensor library found, for working out why something is missing.
    public static string Build(IEnumerable<IHardware> hardware, SensorSnapshot? snapshot)
    {
        var text = new StringBuilder();
        text.AppendLine($"{Loc.T("diag_title")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}");
        text.AppendLine($"{Loc.T("diag_os")}: {Environment.OSVersion}");
        foreach (var line in StatusSummary.SystemLines(snapshot, SystemInfo.IsElevated(), SystemInfo.IsPawnIoInstalled()))
            text.AppendLine(line);

        text.AppendLine();

        AppendSelected(text, snapshot);

        text.AppendLine();
        text.AppendLine(Loc.T("diag_all"));
        foreach (var device in hardware)
            AppendHardware(text, device, depth: 0);

        return text.ToString();
    }

    private static void AppendSelected(StringBuilder text, SensorSnapshot? snapshot)
    {
        text.AppendLine(Loc.T("diag_selected"));

        if (snapshot is null || !snapshot.Devices.Any())
        {
            text.AppendLine($"  {Loc.T("diag_none")}");
            return;
        }

        var temperatureLabel = Loc.T("diag_temp");
        var usageLabel = Loc.T("diag_usage");

        foreach (var metric in snapshot.Devices)
        {
            text.AppendLine(string.Create(CultureInfo.InvariantCulture,
                $"  {MetricLabels.For(metric),-8} {metric.DeviceName,-40} {temperatureLabel}={Format(metric.TemperatureC)} °C  {usageLabel}={Format(metric.UsagePercent)} %"));
        }
    }

    // One device with its sensors, followed by its sub-devices (CPU cores, memory modules) indented one step further.
    private static void AppendHardware(StringBuilder text, IHardware device, int depth)
    {
        var indent = new string(' ', 2 + depth * 2);
        text.AppendLine($"{indent}[{device.HardwareType}] {device.Name}");

        foreach (var sensor in device.Sensors.OrderBy(s => s.SensorType).ThenBy(s => s.Name))
        {
            text.AppendLine(string.Create(CultureInfo.InvariantCulture,
                $"{indent}    {sensor.SensorType,-12} {sensor.Name,-32} {Format(sensor.Value)}"));
        }

        foreach (var sub in device.SubHardware)
            AppendHardware(text, sub, depth + 1);
    }

    private static string Format(double? value) => value is { } v ? v.ToString("0.#", CultureInfo.InvariantCulture) : "-";
}
