namespace FrameHeat.Core.Localization.Translations;

internal static class English
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "Looking for sensors…",
        ["overlay_edit_hint"] = "Drag the widget into place",
        ["overlay_btn_orientation"] = "Switch layout",
        ["overlay_btn_settings"] = "Settings",
        ["overlay_btn_done"] = "Done",
        ["metric_disk"] = "Disk",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – Settings",
        ["settings_intro"] = "Settings for the widget's look, alert limits and position.",

        // Startup
        ["startup_title"] = "Startup",
        ["startup_autostart"] = "Start with Windows",
        ["startup_hint"] = "The widget then appears on its own when Windows starts, so you don't have to open it by hand.",

        // Appearance
        ["look_title"] = "Appearance",
        ["look_design"] = "Style",
        ["look_design_gauge"] = "Ring gauges",
        ["look_design_compact"] = "Compact (number and bar)",
        ["look_orientation"] = "Layout",
        ["look_horizontal"] = "Horizontal",
        ["look_vertical"] = "Vertical",
        ["look_background"] = "Background",
        ["look_bg_glass"] = "Dark glass (translucent)",
        ["look_bg_solid"] = "Solid dark",
        ["look_bg_none"] = "No background, tiles only",
        ["look_size"] = "Size",
        ["look_opacity"] = "Background opacity",
        ["look_disk"] = "Disk percentage",
        ["look_disk_used"] = "Used space",
        ["look_disk_activity"] = "Activity (how busy it is)",
        ["look_accent"] = "Accent color",
        ["look_opacity_hint"] = "Background opacity adjusts the glass background or, when there is no background, the tiles' own background. The solid background is always fully opaque.",
        ["look_show_labels"] = "Show labels and icons",
        ["look_show_usage"] = "Show usage percentages and bars",

        // What to show
        ["show_title"] = "Show in the widget",
        ["show_cpu"] = "Processor (CPU)",
        ["show_gpu"] = "Graphics card (GPU)",
        ["show_vram"] = "VRAM (graphics card memory)",
        ["show_ram"] = "Memory (RAM)",
        ["show_disks"] = "Disks",

        // Position
        ["pos_title"] = "Position",
        ["pos_on_top"] = "Show on top of games and other windows",
        ["pos_on_top_hint"] = "Switch this off and the widget becomes an ordinary window that a game in front of it hides.",
        ["pos_top_left"] = "↖ Top left",
        ["pos_top_right"] = "↗ Top right",
        ["pos_bottom_left"] = "↙ Bottom left",
        ["pos_bottom_right"] = "↘ Bottom right",
        ["pos_edit"] = "Move mode on / off",
        ["pos_hint"] = "In move mode the widget can be dragged anywhere with the mouse, and buttons for switching the layout and opening the settings appear below it. The hotkeys also work in games: Ctrl+Alt+F9 shows or hides the widget, Ctrl+Alt+F10 opens move mode, Ctrl+Alt+F11 switches between horizontal and vertical layout and Ctrl+Alt+F12 opens these settings. On some keyboards the hotkeys work as they are; on others Fn has to be held as well (Ctrl+Alt+Fn+F9). While a game is running, the widget lets all clicks through.",

        // Alert limits
        ["alert_title"] = "Alert limits",
        ["alert_intro"] = "The reading turns yellow as the temperature approaches the limit and pulses red once the limit is exceeded. The defaults suit most computers.",
        ["alert_device_limits"] = "Use the disks' and memory's own limits when the device reports them (recommended)",
        ["alert_device_hint"] = "Then the RAM and disk sliders are only a fallback in case the device doesn't report its own limit.",
        ["alert_yellow"] = "Yellow starts",
        ["alert_red"] = "Red starts",
        ["alert_reset"] = "Restore default limits",

        // Status
        ["status_title"] = "Status",
        ["status_diag"] = "Save diagnostics",
        ["status_demo"] = "Test alert colors",
        ["status_admin_ok"] = "Permissions: administrator ✓",
        ["status_admin_missing"] = "Permissions: standard. CPU and disk temperatures are missing; run the program as administrator.",
        ["status_driver_ok"] = "Driver (CPU and memory temperatures): installed ✓",
        ["status_driver_missing"] = "Driver (CPU and memory temperatures): missing. Install the program from its installer and the driver comes with it.",
        ["status_no_temp"] = "temperature not available",

        // Tray icon
        ["tray_show"] = "Show widget",
        ["tray_edit"] = "Move mode",
        ["tray_orientation"] = "Switch horizontal/vertical",
        ["tray_settings"] = "Settings…",
        ["tray_exit"] = "Exit",
        ["tray_hint_title"] = "FrameHeat is running",
        ["tray_hint_text"] = "Settings: click this icon or press Ctrl+Alt+F12.\nMove and layout: Ctrl+Alt+F10.",

        // Messages
        ["msg_autostart_failed"] = "Starting with Windows could not be changed. It requires administrator permissions.",
        ["msg_autostart_ask"] = "Should FrameHeat start automatically when Windows starts?\n\nYou can change this later in Settings from the icon's menu.",

        // Dialog buttons
        ["button_yes"] = "Yes",
        ["button_no"] = "No",
        ["button_ok"] = "OK",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – diagnostics",
        ["diag_os"] = "Operating system",
        ["diag_selected"] = "Sensors chosen for the tiles:",
        ["diag_none"] = "(no measurements yet)",
        ["diag_all"] = "All devices and sensors found:",
        ["diag_temp"] = "temp",
        ["diag_usage"] = "usage",

        // Frame rate and more appearance options
        ["fps_low"] = "1 % low",
        ["fps_display"] = "Display",
        ["look_design_textrow"] = "Text row (one line per device)",
        ["look_unit"] = "Temperature unit",
        ["look_unit_celsius"] = "Celsius (°C)",
        ["look_unit_fahrenheit"] = "Fahrenheit (°F)",
        ["look_accent_custom"] = "Other color…",
        ["shape_title"] = "Colors and shape",
        ["shape_warning"] = "Warning color",
        ["shape_critical"] = "Critical color",
        ["shape_colors_reset"] = "Restore default colors",
        ["shape_corner"] = "Corner roundness",
        ["shape_spacing"] = "Space between tiles",
        ["shape_text"] = "Text size",
        ["shape_widget_opacity"] = "Widget opacity",
        ["show_fps"] = "FPS (frame rate of the game)",
        ["show_fps_hint"] = "The frame rate is read from the game in the foreground without touching the game. It works with DirectX 10, 11 and 12 games and needs administrator permissions.",
        ["show_order_hint"] = "The arrows change the order of the tiles.",
        ["show_move_up"] = "Move up",
        ["show_move_down"] = "Move down",
        ["show_disks_list"] = "Show these disks",
        ["show_disks_off_hint"] = "Switch on Disks above to choose which disks are shown.",
        ["status_fps_ok"] = "Frame rate (FPS): available ✓",
        ["status_fps_missing"] = "Frame rate (FPS): not available. It needs administrator permissions.",
    };
}
