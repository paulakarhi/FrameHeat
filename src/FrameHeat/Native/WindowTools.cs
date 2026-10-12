using System.Drawing;
using System.Runtime.InteropServices;
using FrameHeat.Layout;
using Screen = System.Windows.Forms.Screen;

namespace FrameHeat.Native;

/// <summary>Window behaviour built from the raw Win32 calls: overlay styles, focus, drag limits, monitor sizes.</summary>
internal static class WindowTools
{
    /// <summary>
    /// Makes a window non-activating, hidden from the taskbar and Alt+Tab and, when
    /// <paramref name="clickThrough"/> is set, transparent to mouse input.
    /// </summary>
    public static void ApplyOverlayStyles(IntPtr hwnd, bool clickThrough)
    {
        var style = User32.GetWindowLong(hwnd, User32.GwlExStyle);
        style |= User32.WsExToolWindow | User32.WsExNoActivate | User32.WsExLayered;
        style = clickThrough ? style | User32.WsExTransparent : style & ~User32.WsExTransparent;
        User32.SetWindowLong(hwnd, User32.GwlExStyle, style);
    }

    /// <summary>Re-asserts topmost order without taking focus.</summary>
    public static void KeepTopmost(IntPtr hwnd)
        => User32.SetWindowPos(hwnd, User32.HwndTopmost, 0, 0, 0, 0,
            User32.SwpNoMove | User32.SwpNoSize | User32.SwpNoActivate);

    /// <summary>
    /// Brings a window to the foreground. Windows refuses focus changes from background
    /// processes, so the calling thread briefly attaches to the foreground thread's input queue.
    /// </summary>
    public static void BringToFront(IntPtr hwnd)
    {
        if (User32.IsIconic(hwnd))
            User32.ShowWindow(hwnd, User32.SwRestore);

        var foregroundThread = User32.GetWindowThreadProcessId(ForegroundWindow(), out _);
        var thisThread = User32.GetCurrentThreadId();
        var attached = foregroundThread != 0 && foregroundThread != thisThread
                       && User32.AttachThreadInput(thisThread, foregroundThread, true);
        try
        {
            User32.BringWindowToTop(hwnd);
            User32.SetForegroundWindow(hwnd);
        }
        finally
        {
            if (attached)
                User32.AttachThreadInput(thisThread, foregroundThread, false);
        }
    }

    /// <summary>
    /// Handles WM_MOVING: keeps the window being dragged inside the monitor under the cursor.
    /// </summary>
    public static void ConstrainDrag(IntPtr rectPointer)
    {
        var rect = Marshal.PtrToStructure<User32.Rect>(rectPointer);
        var window = Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);

        User32.GetCursorPos(out var cursor);
        var bounds = Screen.FromPoint(new Point(cursor.X, cursor.Y)).Bounds;
        var clamped = ScreenGeometry.ClampInside(window, bounds);

        Marshal.StructureToPtr(new User32.Rect
        {
            Left = clamped.Left,
            Top = clamped.Top,
            Right = clamped.Right,
            Bottom = clamped.Bottom,
        }, rectPointer, false);
    }

    /// <summary>The window the user is working in right now, or zero when there is none.</summary>
    public static IntPtr ForegroundWindow() => User32.GetForegroundWindow();

    /// <summary>The process that owns a window, or zero for no window.</summary>
    public static int ProcessIdOf(IntPtr hwnd)
    {
        User32.GetWindowThreadProcessId(hwnd, out var processId);
        return (int)processId;
    }

    /// <summary>
    /// Refresh rate in hertz of the monitor a window is on. Null when it cannot be read; Windows
    /// reports 0 or 1 for "the hardware default".
    /// </summary>
    public static double? RefreshRateHz(IntPtr hwnd)
    {
        var mode = new User32.DevMode { Size = (ushort)Marshal.SizeOf<User32.DevMode>() };
        var monitor = hwnd == IntPtr.Zero ? Screen.PrimaryScreen : Screen.FromHandle(hwnd);
        if (monitor is null || !User32.EnumDisplaySettings(monitor.DeviceName, User32.EnumCurrentSettings, ref mode))
            return null;

        return mode.DisplayFrequency > 1 ? mode.DisplayFrequency : null;
    }

    /// <summary>All monitors together, in pixels, read from the system each time it is called.</summary>
    public static Rectangle VirtualScreenBounds() => System.Windows.Forms.SystemInformation.VirtualScreen;

    public static Rectangle MonitorBounds(IntPtr hwnd) => Screen.FromHandle(hwnd).Bounds;

    public static Rectangle MonitorWorkingArea(IntPtr hwnd) => Screen.FromHandle(hwnd).WorkingArea;
}
