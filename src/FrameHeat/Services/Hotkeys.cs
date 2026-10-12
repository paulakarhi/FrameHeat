namespace FrameHeat.Services;

/// <summary>A Ctrl+Alt hotkey: the virtual key to register and the text the tray menu shows for it.</summary>
internal readonly record struct HotkeyBinding(uint VirtualKey, string Label);

/// <summary>The four hotkeys. They are all Ctrl+Alt plus a function key; the virtual key codes are F9 to F12.</summary>
internal static class Hotkeys
{
    public static readonly HotkeyBinding ToggleVisible = new(0x78, "Ctrl+Alt+F9");
    public static readonly HotkeyBinding ToggleEditMode = new(0x79, "Ctrl+Alt+F10");
    public static readonly HotkeyBinding ToggleOrientation = new(0x7A, "Ctrl+Alt+F11");
    public static readonly HotkeyBinding ShowSettings = new(0x7B, "Ctrl+Alt+F12");
}
