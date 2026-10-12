namespace FrameHeat.Core.Localization.Translations;

internal static class German
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "Sensoren werden gesucht…",
        ["overlay_edit_hint"] = "Widget mit der Maus an die richtige Stelle ziehen",
        ["overlay_btn_orientation"] = "Ausrichtung wechseln",
        ["overlay_btn_settings"] = "Einstellungen",
        ["overlay_btn_done"] = "Fertig",
        ["metric_disk"] = "Datenträger",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – Einstellungen",
        ["settings_intro"] = "Einstellungen für Aussehen, Warngrenzen und Position des Widgets.",

        // Startup
        ["startup_title"] = "Start",
        ["startup_autostart"] = "Mit Windows starten",
        ["startup_hint"] = "Das Widget erscheint dann von selbst, wenn Windows startet, und muss nicht von Hand geöffnet werden.",

        // Appearance
        ["look_title"] = "Aussehen",
        ["look_design"] = "Stil",
        ["look_design_gauge"] = "Ringanzeigen",
        ["look_design_compact"] = "Kompakt (Zahl und Balken)",
        ["look_orientation"] = "Ausrichtung",
        ["look_horizontal"] = "Horizontal",
        ["look_vertical"] = "Vertikal",
        ["look_background"] = "Hintergrund",
        ["look_bg_glass"] = "Dunkles Glas (durchscheinend)",
        ["look_bg_solid"] = "Einfarbig dunkel",
        ["look_bg_none"] = "Kein Hintergrund, nur Kacheln",
        ["look_size"] = "Größe",
        ["look_opacity"] = "Deckkraft des Hintergrunds",
        ["look_disk"] = "Datenträger-Prozentwert",
        ["look_disk_used"] = "Belegter Speicherplatz",
        ["look_disk_activity"] = "Aktivität (wie ausgelastet)",
        ["look_accent"] = "Akzentfarbe",
        ["look_opacity_hint"] = "Die Deckkraft steuert den Glashintergrund oder, wenn kein Hintergrund vorhanden ist, den eigenen Hintergrund der Kacheln. Der einfarbige Hintergrund ist immer vollständig deckend.",
        ["look_show_boxes"] = "Rahmen um jede Kachel anzeigen",
        ["look_show_labels"] = "Bezeichnungen und Symbole anzeigen",
        ["look_show_usage"] = "Auslastung in Prozent und Balken anzeigen",

        // What to show
        ["show_title"] = "Im Widget anzeigen",
        ["show_cpu"] = "Prozessor (CPU)",
        ["show_gpu"] = "Grafikkarte (GPU)",
        ["show_vram"] = "VRAM (Grafikkartenspeicher)",
        ["show_ram"] = "Arbeitsspeicher (RAM)",
        ["show_disks"] = "Datenträger",

        // Position
        ["pos_title"] = "Position",
        ["pos_on_top"] = "Über Spielen und anderen Fenstern anzeigen",
        ["pos_on_top_hint"] = "Ist dies ausgeschaltet, ist das Widget ein gewöhnliches Fenster, das ein Spiel davor verdeckt.",
        ["pos_top_left"] = "↖ Oben links",
        ["pos_top_right"] = "↗ Oben rechts",
        ["pos_bottom_left"] = "↙ Unten links",
        ["pos_bottom_right"] = "↘ Unten rechts",
        ["pos_edit"] = "Verschiebemodus ein / aus",
        ["pos_hint"] = "Im Verschiebemodus lässt sich das Widget mit der Maus beliebig verschieben, und darunter erscheinen Schaltflächen zum Wechseln der Ausrichtung und zum Öffnen der Einstellungen. Die Tastenkürzel funktionieren auch im Spiel: Strg+Alt+F9 zeigt oder verbirgt das Widget, Strg+Alt+F10 öffnet den Verschiebemodus, Strg+Alt+F11 wechselt zwischen horizontaler und vertikaler Ausrichtung, und Strg+Alt+F12 öffnet diese Einstellungen. Auf manchen Tastaturen funktionieren die Kürzel direkt, auf anderen muss zusätzlich Fn gedrückt werden (Strg+Alt+Fn+F9). Im Spiel lässt das Widget alle Klicks durch.",

        // Alert limits
        ["alert_title"] = "Warngrenzen",
        ["alert_intro"] = "Der Wert wird gelb, wenn sich die Temperatur der Grenze nähert, und pulsierend rot, sobald die Grenze überschritten ist. Die Standardwerte passen für die meisten Computer.",
        ["alert_device_limits"] = "Eigene Grenzwerte von Datenträgern und Speicher verwenden, wenn das Gerät sie meldet (empfohlen)",
        ["alert_device_hint"] = "Dann gelten die Regler für RAM und Datenträger nur als Reserve, falls das Gerät keinen eigenen Grenzwert meldet.",
        ["alert_yellow"] = "Gelb ab",
        ["alert_red"] = "Rot ab",
        ["alert_reset"] = "Standardgrenzen wiederherstellen",

        // Status
        ["status_title"] = "Status",
        ["status_diag"] = "Diagnose speichern",
        ["status_demo"] = "Warnfarben testen",
        ["status_admin_ok"] = "Berechtigungen: Administrator ✓",
        ["status_admin_missing"] = "Berechtigungen: Standard. CPU- und Datenträgertemperaturen fehlen; starte das Programm als Administrator.",
        ["status_driver_ok"] = "Treiber (CPU- und Speichertemperaturen): installiert ✓",
        ["status_driver_missing"] = "Treiber (CPU- und Speichertemperaturen): fehlt. Installiere das Programm über das Installationspaket, dann wird der Treiber mitinstalliert.",
        ["status_no_temp"] = "Temperatur nicht verfügbar",

        // Tray icon
        ["tray_show"] = "Widget anzeigen",
        ["tray_edit"] = "Verschiebemodus",
        ["tray_orientation"] = "Horizontal/vertikal wechseln",
        ["tray_settings"] = "Einstellungen…",
        ["tray_exit"] = "Beenden",
        ["tray_hint_title"] = "FrameHeat läuft",
        ["tray_hint_text"] = "Einstellungen: dieses Symbol anklicken oder Strg+Alt+F12 drücken.\nVerschieben und Ausrichtung: Strg+Alt+F10.",

        // Messages
        ["msg_autostart_failed"] = "Der Start mit Windows konnte nicht geändert werden. Dafür sind Administratorrechte erforderlich.",
        ["msg_autostart_ask"] = "Soll FrameHeat automatisch starten, wenn Windows startet?\n\nDie Einstellung lässt sich später im Menü des Symbols unter „Einstellungen“ ändern.",

        // Dialog buttons
        ["button_yes"] = "Ja",
        ["button_no"] = "Nein",
        ["button_ok"] = "OK",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – Diagnose",
        ["diag_os"] = "Betriebssystem",
        ["diag_selected"] = "Für die Kacheln gewählte Sensoren:",
        ["diag_none"] = "(noch keine Messwerte)",
        ["diag_all"] = "Alle gefundenen Geräte und Sensoren:",
        ["diag_temp"] = "Temp.",
        ["diag_usage"] = "Auslastung",

        // Frame rate and more appearance options
        ["fps_low"] = "1 % Low",
        ["fps_display"] = "Monitor",
        ["look_design_textrow"] = "Textzeile (eine Zeile pro Gerät)",
        ["look_unit"] = "Temperatureinheit",
        ["look_unit_celsius"] = "Celsius (°C)",
        ["look_unit_fahrenheit"] = "Fahrenheit (°F)",
        ["look_accent_custom"] = "Andere Farbe…",
        ["shape_title"] = "Farben und Form",
        ["shape_warning"] = "Warnfarbe",
        ["shape_critical"] = "Kritische Farbe",
        ["shape_colors_reset"] = "Standardfarben wiederherstellen",
        ["shape_corner"] = "Eckenrundung",
        ["shape_spacing"] = "Abstand zwischen den Kacheln",
        ["shape_text"] = "Textgröße",
        ["shape_widget_opacity"] = "Deckkraft des Widgets",
        ["show_fps"] = "FPS (Bildrate des Spiels)",
        ["show_fps_hint"] = "Die Bildrate wird vom Spiel im Vordergrund gelesen, ohne das Spiel anzutasten. Das funktioniert mit DirectX-10-, -11- und -12-Spielen und benötigt Administratorrechte.",
        ["show_order_hint"] = "Mit den Pfeilen änderst du die Reihenfolge der Kacheln.",
        ["show_move_up"] = "Nach oben",
        ["show_move_down"] = "Nach unten",
        ["show_disks_list"] = "Diese Datenträger anzeigen",
        ["show_disks_off_hint"] = "Aktivieren Sie oben „Datenträger“, um auszuwählen, welche Datenträger angezeigt werden.",
        ["status_fps_ok"] = "Bildrate (FPS): verfügbar ✓",
        ["status_fps_missing"] = "Bildrate (FPS): nicht verfügbar. Sie benötigt Administratorrechte.",
        ["show_net"] = "Netzwerkgeschwindigkeit",
        ["show_ping"] = "Ping (Verzögerung zu einer Adresse)",
        ["show_ping_hint"] = "Ping sendet einmal pro Sekunde eine kleine Testnachricht an die Adresse darüber. Solange die Ping-Kachel ausgeschaltet ist, passiert nichts.",
        ["show_ping_address"] = "Ping-Adresse",
        ["look_speed"] = "Geschwindigkeitseinheit",
        ["look_speed_mbit"] = "Bit pro Sekunde (Mbit/s)",
        ["look_speed_mbyte"] = "Byte pro Sekunde (MB/s)",
        ["ping_loss"] = "Verloren",
        ["ping_average"] = "Mittel",
        ["net_peak"] = "Höchstwert",
    };
}
