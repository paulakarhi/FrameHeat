namespace FrameHeat.Core.Metrics;

/// <summary>How fast data moves through the network connection in use.</summary>
/// <param name="DownloadBytesPerSecond">Bytes received per second.</param>
/// <param name="UploadBytesPerSecond">Bytes sent per second.</param>
/// <param name="LinkSpeedBitsPerSecond">What the connection is rated for, when the adapter reports it.</param>
/// <param name="PeakDownloadBytesPerSecond">The highest download speed seen so far.</param>
public sealed record NetworkStats(
    double DownloadBytesPerSecond,
    double UploadBytesPerSecond,
    double? LinkSpeedBitsPerSecond,
    double PeakDownloadBytesPerSecond);
