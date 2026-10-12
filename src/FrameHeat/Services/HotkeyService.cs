using System.Windows.Interop;
using FrameHeat.Native;

namespace FrameHeat.Services;

/// <summary>
/// Global hotkeys. They are received by a hidden message-only window, so they work while the
/// widget is hidden and while a game has focus.
/// </summary>
internal sealed class HotkeyService : IDisposable
{
    private const uint Modifiers = User32.ModControl | User32.ModAlt | User32.ModNoRepeat;

    private readonly HwndSource _source;

    // Registered hotkey id -> what to do when it is pressed.
    private readonly Dictionary<int, Action> _actions = new();
    private int _nextId = 1;

    // The message-only window has no visible part; it exists only to receive WM_HOTKEY.
    public HotkeyService()
    {
        var parameters = new HwndSourceParameters("FrameHeatHotkeys")
        {
            ParentWindow = User32.HwndMessage,
            WindowStyle = 0,
        };

        _source = new HwndSource(parameters);
        _source.AddHook(WndProc);
    }

    /// <summary>Returns false when another program already owns the key combination.</summary>
    public bool Register(HotkeyBinding hotkey, Action action)
    {
        var id = _nextId++;

        if (!User32.RegisterHotKey(_source.Handle, id, Modifiers, hotkey.VirtualKey))
        {
            Log.Error($"Hotkey registration failed ({hotkey.Label})");
            return false;
        }

        _actions[id] = action;
        return true;
    }

    // Looks up the pressed hotkey by id and runs its action.
    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == User32.WmHotkey && _actions.TryGetValue(wParam.ToInt32(), out var action))
        {
            action();
            handled = true;
        }

        return IntPtr.Zero;
    }

    public void Dispose()
    {
        foreach (var id in _actions.Keys)
            User32.UnregisterHotKey(_source.Handle, id);

        _actions.Clear();
        _source.Dispose();
    }
}
