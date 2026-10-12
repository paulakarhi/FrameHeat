using System.Text;
using FrameHeat.ViewModels;

namespace FrameHeat.Sensors;

/// <summary>The short status text shown in the settings window.</summary>
internal static class StatusSummary
{
    // First the things that decide what can be read (permissions, driver, frame rate), then one line per detected device.
    public static string Build(SensorSnapshot? snapshot, bool isElevated, bool isDriverInstalled, TemperatureUnit unit)
    {
        var text = new StringBuilder();
        foreach (var line in SystemLines(snapshot, isElevated, isDriverInstalled))
            text.AppendLine(line);

        if (snapshot?.Devices.Any() == true)
        {
            text.AppendLine();
            foreach (var metric in snapshot.Devices)
            {
                var temperature = metric.TemperatureC is { } value
                    ? Temperature.Format(value, unit)
                    : Loc.T("status_no_temp");
                var label = MetricLabels.For(metric);
                var device = metric.DeviceName == label ? "" : $"{metric.DeviceName}, ";
                text.AppendLine($"{label}: {device}{temperature}");
            }
        }

        return text.ToString().TrimEnd();
    }

    /// <summary>
    /// One line each for the three things that decide what can be read: permissions, the driver and the
    /// frame rate. The frame rate is left out until the first sensor reading exists.
    /// </summary>
    internal static IEnumerable<string> SystemLines(SensorSnapshot? snapshot, bool isElevated, bool isDriverInstalled)
    {
        yield return Loc.T(isElevated ? "status_admin_ok" : "status_admin_missing");
        yield return Loc.T(isDriverInstalled ? "status_driver_ok" : "status_driver_missing");

        if (snapshot is not null)
            yield return Loc.T(snapshot.HasFrameRate ? "status_fps_ok" : "status_fps_missing");
    }
}
