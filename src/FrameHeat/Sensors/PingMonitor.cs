using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace FrameHeat.Sensors;

/// <summary>
/// Measures the delay to the address chosen in the settings by sending it a small test message once a
/// second. This is the only thing in the program that uses the network, so it runs only while the Ping
/// tile is on, and starts over when the address changes.
/// </summary>
internal sealed class PingMonitor(AppSettings settings) : IDisposable
{
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(1);
    private const int TimeoutMilliseconds = 1000;

    private readonly object _gate = new();
    private PingTracker _tracker = new();
    private CancellationTokenSource? _loop;
    private string? _target;

    public void Start()
    {
        settings.Changed += OnSettingsChanged;
        Apply();
    }

    /// <summary>The figures over the latest messages; null while the tile is off and before the first message.</summary>
    public PingStats? Read()
    {
        lock (_gate)
            return _tracker.Read();
    }

    public void Dispose()
    {
        settings.Changed -= OnSettingsChanged;
        lock (_gate)
            Stop();
    }

    private void OnSettingsChanged(SettingsChange change)
    {
        if (change.Area is SettingsArea.Tiles or SettingsArea.General)
            Apply();
    }

    // Pings the address while the tile is on. A different state than before means starting over.
    private void Apply()
    {
        lock (_gate)
        {
            var target = settings.Tiles.Ping ? settings.PingTarget : null;
            if (target == _target)
                return;

            Stop();
            _target = target;
            if (target is null)
                return;

            _loop = new CancellationTokenSource();
            var tracker = _tracker;
            var token = _loop.Token;
            Task.Run(() => RunAsync(target, tracker, token));
        }
    }

    // Must be called with the lock held.
    private void Stop()
    {
        _loop?.Cancel();
        _loop?.Dispose();
        _loop = null;
        _target = null;
        _tracker = new PingTracker();
    }

    private static async Task RunAsync(string target, PingTracker tracker, CancellationToken cancellation)
    {
        using var ping = new Ping();
        using var timer = new PeriodicTimer(Interval);
        IPAddress? address = null;

        // A name that cannot be found, or a connection that is down, counts as a lost message.
        async Task<double?> ProbeAsync()
        {
            try
            {
                address ??= (await Dns.GetHostAddressesAsync(target, cancellation)).FirstOrDefault();
                if (address is null)
                    return null;

                var reply = await ping.SendPingAsync(address, TimeoutMilliseconds);
                return reply.Status == IPStatus.Success ? reply.RoundtripTime : null;
            }
            catch (Exception ex) when (ex is SocketException or PingException or InvalidOperationException)
            {
                address = null;
                return null;
            }
        }

        try
        {
            do
            {
                tracker.Add(await ProbeAsync());
            }
            while (await timer.WaitForNextTickAsync(cancellation));
        }
        catch (OperationCanceledException)
        {
            // Cancelling is how the loop is stopped.
        }
    }
}
