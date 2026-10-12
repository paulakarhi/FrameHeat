namespace FrameHeat.Core.Metrics;

/// <summary>
/// One device's measurements at a point in time. A null value means the sensor is missing or unreadable.
/// </summary>
/// <param name="Key">Stable identity of the tile across updates.</param>
/// <param name="UsagePercent">Load, or used space for disks.</param>
/// <param name="ActivityPercent">Disks only: how busy the drive is right now.</param>
/// <param name="DeviceLimitC">Temperature at which the device itself reports overheating, when it exposes one.</param>
/// <param name="Ordinal">Disk number when several disks are shown, otherwise 0.</param>
/// <param name="Frames">The frame rate figures of an FPS reading; null when no game is running.</param>
public sealed record MetricReading(
    string Key,
    MetricKind Kind,
    string DeviceName,
    double? TemperatureC,
    double? UsagePercent,
    double? ActivityPercent = null,
    double? DeviceLimitC = null,
    int Ordinal = 0,
    FrameStats? Frames = null);
