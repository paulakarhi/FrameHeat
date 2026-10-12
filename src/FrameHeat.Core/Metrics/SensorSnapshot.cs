namespace FrameHeat.Core.Metrics;

/// <summary>All readings taken at one moment. The sensor thread builds it and nobody changes it afterwards.</summary>
public sealed record SensorSnapshot(DateTimeOffset Timestamp, IReadOnlyList<MetricReading> Metrics)
{
    /// <summary>True when the frame rate can be read at all, whether or not a game is running right now.</summary>
    public bool HasFrameRate => Metrics.Any(metric => metric.Kind == MetricKind.Fps);

    /// <summary>The readings of real devices, without the frame rate, the network speed and the ping.</summary>
    public IEnumerable<MetricReading> Devices => Metrics.Where(metric => MetricKinds.WithTemperature.Contains(metric.Kind));
}
