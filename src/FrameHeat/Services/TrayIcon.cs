using System.Windows;
using FrameHeat.Commands;
using Forms = System.Windows.Forms;

namespace FrameHeat.Services;

/// <summary>
/// The notification-area icon and its menu. The widget ignores the mouse during play, so this
/// is where it is controlled from.
/// </summary>
internal sealed class TrayIcon : IDisposable
{
    private readonly Forms.NotifyIcon _icon;
    private readonly Forms.ContextMenuStrip _menu;

    public TrayIcon(IAppCommands commands)
    {
        // Follow the Windows light/dark theme. Must run before the first WinForms object is created.
        Forms.Application.SetColorMode(Forms.SystemColorMode.System);
        _menu = new Forms.ContextMenuStrip();

        // Menu items; the hotkey, where there is one, is shown beside the name.
        var visible = Item(Loc.T("tray_show"), Hotkeys.ToggleVisible, commands.ToggleVisible);
        var edit = Item(Loc.T("tray_edit"), Hotkeys.ToggleEditMode, commands.ToggleEditMode);
        var orientation = Item(Loc.T("tray_orientation"), Hotkeys.ToggleOrientation, commands.ToggleOrientation);
        var demo = Item(Loc.T("status_demo"), null, commands.ToggleDemo);
        var autoStart = Item(Loc.T("startup_autostart"), null, commands.ToggleAutoStart);
        var settings = Item(Loc.T("tray_settings"), Hotkeys.ShowSettings, commands.ShowSettings);
        var diagnostics = Item(Loc.T("status_diag"), null, commands.SaveDiagnostics);
        var exit = Item(Loc.T("tray_exit"), null, commands.Exit);

        _menu.Items.AddRange([
            visible, edit, orientation, demo, new Forms.ToolStripSeparator(),
            autoStart, settings, diagnostics, new Forms.ToolStripSeparator(), exit]);

        // State can change through hotkeys, so the check marks are refreshed when the menu opens.
        _menu.Opening += (_, _) =>
        {
            visible.Checked = commands.IsVisible;
            edit.Checked = commands.IsEditMode;
            demo.Checked = commands.IsDemoRunning;
            autoStart.Checked = commands.Settings.StartWithWindows;
        };

        // Left click opens the settings; the menu opens on right click.
        _icon = new Forms.NotifyIcon
        {
            Icon = LoadIcon(),
            Text = AppIdentity.Name,
            ContextMenuStrip = _menu,
            Visible = true,
        };
        _icon.MouseClick += (_, e) =>
        {
            if (e.Button == Forms.MouseButtons.Left)
                commands.ShowSettings();
        };
    }

    /// <summary>
    /// Tells the user where settings and move mode are. The icon may sit behind the overflow
    /// arrow, and a click-through widget gives no hint of its own.
    /// </summary>
    public void ShowStartupHint()
        => _icon.ShowBalloonTip(8000, Loc.T("tray_hint_title"), Loc.T("tray_hint_text"), Forms.ToolTipIcon.Info);

    public void Dispose()
    {
        // Without hiding it first, the icon lingers in the tray until the mouse passes over it.
        _icon.Visible = false;
        _icon.Dispose();
        _menu.Dispose();
    }

    // The hotkey, when there is one, is shown right-aligned after a tab.
    private static Forms.ToolStripMenuItem Item(string text, HotkeyBinding? hotkey, Action action)
        => new(hotkey is { } binding ? $"{text}\t{binding.Label}" : text, null, (_, _) => action());

    private static System.Drawing.Icon LoadIcon()
    {
        using var stream = Application.GetResourceStream(new Uri("pack://application:,,,/FrameHeat;component/Assets/app.ico"))!.Stream;
        return new System.Drawing.Icon(stream);
    }
}
