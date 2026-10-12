using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace FrameHeat.Sensors;

/// <summary>
/// Reads how fast data moves through the network connection in use from the byte counters Windows keeps
/// for every adapter. Nothing is sent anywhere.
/// </summary>
internal sealed class NetworkMonitor
{
    // After a longer pause, for example when the tile was off or the computer slept, the old counters
    // say nothing about the speed right now, so the measuring starts over.
    private static readonly TimeSpan LongestGap = TimeSpan.FromSeconds(3);

    private readonly Stopwatch _clock = Stopwatch.StartNew();
    private NetworkTracker _tracker = new();
    private TimeSpan _lastRead;

    /// <summary>The speed since the previous call; null on the first call and when no connection is up.</summary>
    public NetworkStats? Read()
    {
        var now = _clock.Elapsed;
        var elapsed = now - _lastRead;
        _lastRead = now;

        if (elapsed > LongestGap)
            _tracker = new NetworkTracker();

        return _tracker.Update(Adapters(), elapsed.TotalSeconds);
    }

    // Adapters that are up and have a default gateway. That leaves out the loopback adapter and the virtual
    // switches of the machine, which carry no traffic of their own.
    private static List<AdapterCounters> Adapters()
    {
        try
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(adapter => adapter.OperationalStatus == OperationalStatus.Up
                                  && adapter.NetworkInterfaceType is not (NetworkInterfaceType.Loopback or NetworkInterfaceType.Tunnel)
                                  && HasGateway(adapter))
                .Select(adapter =>
                {
                    var counters = adapter.GetIPStatistics();
                    return new AdapterCounters(
                        adapter.Id,
                        (ulong)counters.BytesReceived,
                        (ulong)counters.BytesSent,
                        adapter.Speed > 0 ? adapter.Speed : null);
                })
                .ToList();
        }
        catch (NetworkInformationException)
        {
            // Windows could not list the adapters this time; the next reading tries again.
            return [];
        }
    }

    private static bool HasGateway(NetworkInterface adapter)
        => adapter.GetIPProperties().GatewayAddresses.Any(gateway =>
            !gateway.Address.Equals(IPAddress.Any) && !gateway.Address.Equals(IPAddress.IPv6Any));
}
