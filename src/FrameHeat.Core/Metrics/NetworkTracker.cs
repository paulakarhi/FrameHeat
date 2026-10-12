namespace FrameHeat.Core.Metrics;

/// <summary>
/// Turns the byte counters of the network adapters into a transfer speed. Only the busiest adapter counts:
/// a VPN carries the same data as the connection under it, so adding the two would count everything twice.
/// </summary>
public sealed class NetworkTracker
{
    private Dictionary<string, AdapterCounters> _previous = [];
    private double _peakDownload;

    /// <summary>
    /// The speed since the previous call, with the highest download speed seen so far. Null when no adapter has
    /// been seen before, which is the case on the first call, and when an adapter's counters went backwards,
    /// for example after a reset.
    /// </summary>
    public NetworkStats? Update(IReadOnlyList<AdapterCounters> adapters, double elapsedSeconds)
    {
        NetworkStats? busiest = null;

        foreach (var adapter in adapters)
        {
            if (elapsedSeconds <= 0 || !_previous.TryGetValue(adapter.Id, out var before)
                || adapter.BytesReceived < before.BytesReceived || adapter.BytesSent < before.BytesSent)
                continue;

            var speed = new NetworkStats(
                (adapter.BytesReceived - before.BytesReceived) / elapsedSeconds,
                (adapter.BytesSent - before.BytesSent) / elapsedSeconds,
                adapter.LinkSpeedBitsPerSecond,
                PeakDownloadBytesPerSecond: 0);

            if (busiest is null || Total(speed) > Total(busiest))
                busiest = speed;
        }

        _previous = adapters.ToDictionary(adapter => adapter.Id);
        if (busiest is null)
            return null;

        _peakDownload = Math.Max(_peakDownload, busiest.DownloadBytesPerSecond);
        return busiest with { PeakDownloadBytesPerSecond = _peakDownload };
    }

    private static double Total(NetworkStats speed) => speed.DownloadBytesPerSecond + speed.UploadBytesPerSecond;
}
