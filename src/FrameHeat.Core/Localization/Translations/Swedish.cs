namespace FrameHeat.Core.Localization.Translations;

internal static class Swedish
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "Söker efter sensorer…",
        ["overlay_edit_hint"] = "Dra widgeten till önskad plats",
        ["overlay_btn_orientation"] = "Byt riktning",
        ["overlay_btn_settings"] = "Inställningar",
        ["overlay_btn_done"] = "Klar",
        ["metric_disk"] = "Disk",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – Inställningar",
        ["settings_intro"] = "Inställningar för widgetens utseende, varningsgränser och position.",

        // Startup
        ["startup_title"] = "Start",
        ["startup_autostart"] = "Starta med Windows",
        ["startup_hint"] = "Då visas widgeten av sig själv när Windows startar, så du behöver inte öppna den manuellt.",

        // Appearance
        ["look_title"] = "Utseende",
        ["look_design"] = "Stil",
        ["look_design_gauge"] = "Ringmätare",
        ["look_design_compact"] = "Kompakt (siffra och stapel)",
        ["look_orientation"] = "Riktning",
        ["look_horizontal"] = "Vågrät",
        ["look_vertical"] = "Lodrät",
        ["look_background"] = "Bakgrund",
        ["look_bg_glass"] = "Mörkt glas (genomskinligt)",
        ["look_bg_solid"] = "Heltäckande mörk",
        ["look_bg_none"] = "Ingen bakgrund, bara rutor",
        ["look_size"] = "Storlek",
        ["look_opacity"] = "Bakgrundens opacitet",
        ["look_disk"] = "Diskens procent",
        ["look_disk_used"] = "Använt utrymme",
        ["look_disk_activity"] = "Aktivitet (hur upptagen den är)",
        ["look_accent"] = "Accentfärg",
        ["look_opacity_hint"] = "Bakgrundens opacitet styr glasbakgrunden eller, när det inte finns någon bakgrund, rutornas egen bakgrund. Den heltäckande bakgrunden är alltid helt ogenomskinlig.",
        ["look_show_boxes"] = "Visa en ram runt varje ruta",
        ["look_show_labels"] = "Visa etiketter och ikoner",
        ["look_show_usage"] = "Visa användningsprocent och staplar",

        // What to show
        ["show_title"] = "Visa i widgeten",
        ["show_cpu"] = "Processor (CPU)",
        ["show_gpu"] = "Grafikkort (GPU)",
        ["show_vram"] = "VRAM (grafikkortets minne)",
        ["show_ram"] = "Minne (RAM)",
        ["show_disks"] = "Diskar",

        // Position
        ["pos_title"] = "Placering",
        ["pos_on_top"] = "Visa ovanpå spel och andra fönster",
        ["pos_on_top_hint"] = "Slår du av detta blir widgeten ett vanligt fönster som ett spel framför döljer.",
        ["pos_top_left"] = "↖ Uppe till vänster",
        ["pos_top_right"] = "↗ Uppe till höger",
        ["pos_bottom_left"] = "↙ Nere till vänster",
        ["pos_bottom_right"] = "↘ Nere till höger",
        ["pos_edit"] = "Flyttläge på / av",
        ["pos_hint"] = "I flyttläget kan widgeten dras vart som helst med musen, och under den visas knappar för att byta riktning och öppna inställningarna. Kortkommandona fungerar även i spel: Ctrl+Alt+F9 visar eller döljer widgeten, Ctrl+Alt+F10 öppnar flyttläget, Ctrl+Alt+F11 byter mellan vågrät och lodrät layout och Ctrl+Alt+F12 öppnar de här inställningarna. På vissa tangentbord fungerar kortkommandona som de är, på andra måste Fn hållas ned också (Ctrl+Alt+Fn+F9). Under spel släpper widgeten igenom alla klick.",

        // Alert limits
        ["alert_title"] = "Larmgränser",
        ["alert_intro"] = "Värdet blir gult när temperaturen närmar sig gränsen och pulserande rött när gränsen överskrids. Standardvärdena passar de flesta datorer.",
        ["alert_device_limits"] = "Använd diskarnas och minnets egna gränser när enheten anger dem (rekommenderas)",
        ["alert_device_hint"] = "Då är reglagen för RAM och diskar bara en reserv om enheten inte anger någon egen gräns.",
        ["alert_yellow"] = "Gult börjar",
        ["alert_red"] = "Rött börjar",
        ["alert_reset"] = "Återställ standardgränser",

        // Status
        ["status_title"] = "Status",
        ["status_diag"] = "Spara diagnostik",
        ["status_demo"] = "Testa larmfärger",
        ["status_admin_ok"] = "Behörighet: administratör ✓",
        ["status_admin_missing"] = "Behörighet: standard. CPU- och disktemperaturer saknas; kör programmet som administratör.",
        ["status_driver_ok"] = "Drivrutin (CPU- och minnestemperaturer): installerad ✓",
        ["status_driver_missing"] = "Drivrutin (CPU- och minnestemperaturer): saknas. Installera programmet från installationspaketet så följer drivrutinen med.",
        ["status_no_temp"] = "temperatur saknas",

        // Tray icon
        ["tray_show"] = "Visa widgeten",
        ["tray_edit"] = "Flyttläge",
        ["tray_orientation"] = "Byt vågrät/lodrät",
        ["tray_settings"] = "Inställningar…",
        ["tray_exit"] = "Avsluta",
        ["tray_hint_title"] = "FrameHeat körs",
        ["tray_hint_text"] = "Inställningar: klicka på den här ikonen eller tryck Ctrl+Alt+F12.\nFlytta och byt riktning: Ctrl+Alt+F10.",

        // Messages
        ["msg_autostart_failed"] = "Det gick inte att ändra start med Windows. Det kräver administratörsbehörighet.",
        ["msg_autostart_ask"] = "Ska FrameHeat starta automatiskt när Windows startar?\n\nDu kan ändra detta senare under Inställningar i ikonens meny.",

        // Dialog buttons
        ["button_yes"] = "Ja",
        ["button_no"] = "Nej",
        ["button_ok"] = "OK",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – diagnostik",
        ["diag_os"] = "Operativsystem",
        ["diag_selected"] = "Sensorer valda för rutorna:",
        ["diag_none"] = "(inga mätningar än)",
        ["diag_all"] = "Alla hittade enheter och sensorer:",
        ["diag_temp"] = "temp",
        ["diag_usage"] = "användning",

        // Frame rate and more appearance options
        ["fps_low"] = "1 % low",
        ["fps_display"] = "Skärm",
        ["look_design_textrow"] = "Textrad (en rad per enhet)",
        ["look_unit"] = "Temperaturenhet",
        ["look_unit_celsius"] = "Celsius (°C)",
        ["look_unit_fahrenheit"] = "Fahrenheit (°F)",
        ["look_accent_custom"] = "Annan färg…",
        ["shape_title"] = "Färger och form",
        ["shape_warning"] = "Varningsfärg",
        ["shape_critical"] = "Kritisk färg",
        ["shape_colors_reset"] = "Återställ standardfärger",
        ["shape_corner"] = "Hörnrundning",
        ["shape_spacing"] = "Avstånd mellan rutorna",
        ["shape_text"] = "Textstorlek",
        ["shape_widget_opacity"] = "Widgetens opacitet",
        ["show_fps"] = "FPS (spelets bildfrekvens)",
        ["show_fps_hint"] = "Bildfrekvensen läses från spelet i förgrunden utan att röra spelet. Den fungerar med DirectX 10-, 11- och 12-spel och kräver administratörsbehörighet.",
        ["show_order_hint"] = "Pilarna ändrar rutornas ordning.",
        ["show_move_up"] = "Flytta upp",
        ["show_move_down"] = "Flytta ned",
        ["show_disks_list"] = "Visa dessa diskar",
        ["show_disks_off_hint"] = "Slå på Diskar ovan för att välja vilka diskar som visas.",
        ["status_fps_ok"] = "Bildfrekvens (FPS): tillgänglig ✓",
        ["status_fps_missing"] = "Bildfrekvens (FPS): inte tillgänglig. Den kräver administratörsbehörighet.",
        ["show_net"] = "Nätverkshastighet",
        ["show_ping"] = "Ping (fördröjning till en adress)",
        ["show_ping_hint"] = "Ping skickar ett litet testmeddelande en gång i sekunden till adressen ovan. Den gör ingenting förrän Ping-rutan är påslagen.",
        ["show_ping_address"] = "Pingadress",
        ["look_speed"] = "Hastighetsenhet",
        ["look_speed_mbit"] = "Bit per sekund (Mbit/s)",
        ["look_speed_mbyte"] = "Byte per sekund (MB/s)",
        ["ping_loss"] = "Förlorat",
        ["ping_average"] = "Medel",
        ["net_peak"] = "Högst",
    };
}
