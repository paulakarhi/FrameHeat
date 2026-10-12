using FrameHeat.Services;
using LibreHardwareMonitor.Hardware;

namespace FrameHeat.Sensors;

/// <summary>
/// Owns the sensor library and polls it once a second on a background thread. Consumers receive
/// immutable snapshots and never touch the library directly.
/// </summary>
internal sealed class HardwareMonitor(AppSettings settings) : IDisposable
{
    // Drive SMART reads are heavier than the rest and drive temperatures change slowly.
    private const int StorageReadInterval = 5;

    // The sensor library is not thread-safe; the polling loop and the report builder take turns.
    private readonly object _gate = new();
    private readonly CancellationTokenSource _cancellation = new();
    private readonly FpsMonitor _fps = new();
    private readonly NetworkMonitor _network = new();
    private readonly PingMonitor _ping = new(settings);
    private readonly Computer _computer = new()
    {
        IsCpuEnabled = true,
        IsGpuEnabled = true,
        IsMemoryEnabled = true,
        IsStorageEnabled = true,
    };

    private Task? _loop;
    private bool _opened;
    private SensorSnapshot? _latest;

    /// <summary>Raised on the polling thread.</summary>
    public event Action<SensorSnapshot>? SnapshotReady;

    public SensorSnapshot? Latest => Volatile.Read(ref _latest);

    /// <summary>Opens the sensors and begins polling in the background.</summary>
    public void Start()
    {
        _fps.Start();
        _ping.Start();
        _loop = Task.Run(() => RunAsync(_cancellation.Token));
    }

    /// <summary>
    /// Opens the sensors and samples twice, one second apart, because load sensors need two readings.
    /// Used for the report mode that runs without a polling loop.
    /// </summary>
    public void SampleForReport()
    {
        Open();
        Publish(includeStorage: true);
        Thread.Sleep(1000);
        Publish(includeStorage: true);
    }

    /// <summary>Text listing of every device and sensor, for the diagnostics file.</summary>
    public string BuildReport()
    {
        lock (_gate)
            return DiagnosticsReport.Build(_computer.Hardware, Latest);
    }

    // The polling loop: one reading per second until the monitor is disposed. A failed read is
    // logged and the next tick tries again.
    private async Task RunAsync(CancellationToken cancellation)
    {
        Open();

        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        var tick = 0;

        try
        {
            do
            {
                try
                {
                    Publish(includeStorage: tick++ % StorageReadInterval == 0);
                }
                catch (Exception ex)
                {
                    Log.Error("Sensor read failed", ex);
                }
            }
            while (await timer.WaitForNextTickAsync(cancellation));
        }
        catch (OperationCanceledException)
        {
            // Cancelling is how the loop is stopped.
        }
    }

    // Opens the sensor library once. It can fail (for example without a driver); the program then
    // simply shows whatever is readable.
    private void Open()
    {
        lock (_gate)
        {
            if (_opened)
                return;

            try
            {
                _computer.Open();
                _opened = true;
                Log.Info($"Sensors opened (elevated: {SystemInfo.IsElevated()}, PawnIO: {SystemInfo.IsPawnIoInstalled()})");
            }
            catch (Exception ex)
            {
                Log.Error("Opening the sensors failed", ex);
            }
        }
    }

    // Reads the hardware, turns it into a snapshot and hands it to the listeners.
    private void Publish(bool includeStorage)
    {
        IReadOnlyList<MetricReading> readings;

        lock (_gate)
        {
            foreach (var hardware in _computer.Hardware)
                Update(hardware, includeStorage);

            readings = SensorPicker.Pick(_computer.Hardware);
        }

        var snapshot = new SensorSnapshot(DateTimeOffset.Now, WithExtraReadings(readings));
        Volatile.Write(ref _latest, snapshot);
        SnapshotReady?.Invoke(snapshot);
    }

    // The frame rate comes from event tracing and the network figures from Windows' own counters and from
    // ping messages, not from the sensor library. A reading exists only when its source works and, for the
    // network and the ping, the tile is on, so nothing is measured or sent for a tile nobody sees.
    private List<MetricReading> WithExtraReadings(IReadOnlyList<MetricReading> readings)
    {
        var all = readings.ToList();

        if (_fps.IsRunning)
            all.Add(new MetricReading("fps", MetricKind.Fps, "FPS", null, null, Frames: _fps.Read()));

        if (settings.Tiles.Network)
            all.Add(new MetricReading("network", MetricKind.Network, "Network", null, null, Network: _network.Read()));

        if (settings.Tiles.Ping)
            all.Add(new MetricReading("ping", MetricKind.Ping, "Ping", null, null, Ping: _ping.Read()));

        return all;
    }

    // Refreshes a device and its sub-devices; drives are skipped on the ticks that do not read storage.
    private static void Update(IHardware hardware, bool includeStorage)
    {
        if (hardware.HardwareType == HardwareType.Storage && !includeStorage)
            return;

        hardware.Update();
        foreach (var sub in hardware.SubHardware)
            Update(sub, includeStorage);
    }

    // Stops the loop, waits briefly for it to finish, then releases the sensor library.
    public void Dispose()
    {
        _cancellation.Cancel();
        _fps.Dispose();
        _ping.Dispose();

        try
        {
            _loop?.Wait(TimeSpan.FromSeconds(2));
        }
        catch (AggregateException)
        {
            // The loop has already logged what went wrong; shutting down continues regardless.
        }

        lock (_gate)
        {
            if (_opened)
                CloseComputer();
            _opened = false;
        }

        _cancellation.Dispose();
    }

    private void CloseComputer()
    {
        try
        {
            _computer.Close();
        }
        catch (Exception ex)
        {
            Log.Error("Closing the sensors failed", ex);
        }
    }
}
