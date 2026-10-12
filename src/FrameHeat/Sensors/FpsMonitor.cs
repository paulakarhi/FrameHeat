using System.Diagnostics;
using FrameHeat.Native;
using FrameHeat.Services;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace FrameHeat.Sensors;

/// <summary>
/// Reads the frame rate of the game in the foreground without touching the game. Windows reports
/// every frame a program hands to the graphics system through event tracing, the same source
/// PresentMon uses. Listening needs administrator rights, or membership in the Performance Log Users group.
/// </summary>
internal sealed class FpsMonitor : IDisposable
{
    // Provider Microsoft-Windows-DXGI. A frame shows up as a Present call, or as PresentMultiplaneOverlay
    // when the game flips through a hardware overlay. Only those events are asked for (keyword 0x2).
    private static readonly Guid DxgiProvider = new("CA11C036-0102-4A2D-A6AD-F03CFED5D3C9");
    private const ulong PresentKeyword = 0x2;
    private const int PresentStart = 42;
    private const int PresentMultiplaneOverlayStart = 45;

    private const string DefaultSessionName = "FrameHeat.Fps";

    private readonly FrameTracker _frames = new();

    // The clock the arrival of events is measured on: it does not jump when the system clock is adjusted.
    private readonly Stopwatch _clock = Stopwatch.StartNew();

    private readonly string _sessionName;
    private volatile TraceEventSession? _session;

    /// <summary>A session name is taken over by whoever starts it last, so every listener needs a name of its own.</summary>
    public FpsMonitor(string sessionName = DefaultSessionName) => _sessionName = sessionName;

    /// <summary>False when the listener could not be started, for example without the rights to trace events.</summary>
    public bool IsRunning => _session is not null;

    public void Start()
    {
        try
        {
            var session = new TraceEventSession(_sessionName) { StopOnDispose = true };
            session.EnableProvider(DxgiProvider, TraceEventLevel.Always, PresentKeyword);
            session.Source.AllEvents += OnEvent;
            _session = session;

            // Listening blocks until the session ends, so it gets a thread of its own.
            Task.Factory.StartNew(() => Listen(session), TaskCreationOptions.LongRunning);
        }
        catch (Exception ex)
        {
            Log.Error("The frame rate could not be read", ex);
            _session?.Dispose();
            _session = null;
        }
    }

    /// <summary>Frame rate of the program in the foreground; null when it is not drawing frames.</summary>
    public FrameStats? Read() => Read(WindowTools.ForegroundWindow());

    /// <summary>Frame rate of the program that owns the given window.</summary>
    public FrameStats? Read(IntPtr window)
    {
        var processId = WindowTools.ProcessIdOf(window);
        if (processId == 0 || processId == Environment.ProcessId)
            return null;

        var now = Arrival();
        _frames.Prune(now);
        return _frames.Read(processId, now, WindowTools.RefreshRateHz(window));
    }

    public void Dispose()
    {
        // The field is cleared first, so the listening thread can tell this stop from one done by someone else.
        var session = _session;
        _session = null;
        session?.Dispose();
    }

    // Returns when the session ends. Stopping it ourselves ends the wait with an exception, which is
    // expected. It also ends when another listener starts under the same name; that is logged, and the
    // session is not disposed, since that would stop the one that took the name.
    private void Listen(TraceEventSession session)
    {
        try
        {
            session.Source.Process();
        }
        catch (Exception ex) when (ReferenceEquals(_session, session))
        {
            Log.Error("The frame rate listener was stopped from outside", ex);
        }
        catch (Exception)
        {
            // Raised by our own Dispose.
        }
        finally
        {
            if (ReferenceEquals(_session, session))
                _session = null;
        }
    }

    private void OnEvent(TraceEvent data)
    {
        if ((int)data.ID is PresentStart or PresentMultiplaneOverlayStart)
            _frames.OnPresent(data.ProcessID, Seconds(data.TimeStamp), Arrival());
    }

    private double Arrival() => _clock.Elapsed.TotalSeconds;

    private static double Seconds(DateTime time) => time.Ticks / (double)TimeSpan.TicksPerSecond;
}
