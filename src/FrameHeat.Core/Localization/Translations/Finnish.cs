namespace FrameHeat.Core.Localization.Translations;

internal static class Finnish
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "Haetaan mittareita…",
        ["overlay_edit_hint"] = "Raahaa mittari hiirellä paikalleen",
        ["overlay_btn_orientation"] = "Vaihda suunta",
        ["overlay_btn_settings"] = "Asetukset",
        ["overlay_btn_done"] = "Valmis",
        ["metric_disk"] = "Levy",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – asetukset",
        ["settings_intro"] = "Mittarin ulkoasun, hälytysrajojen ja sijainnin asetukset.",

        // Startup
        ["startup_title"] = "Käynnistys",
        ["startup_autostart"] = "Käynnistä Windowsin mukana",
        ["startup_hint"] = "Silloin mittari tulee näkyviin itsestään, kun Windows käynnistyy, eikä sitä tarvitse avata käsin.",

        // Appearance
        ["look_title"] = "Ulkoasu",
        ["look_design"] = "Tyyli",
        ["look_design_gauge"] = "Rengasmittarit",
        ["look_design_compact"] = "Kompakti (numero ja palkki)",
        ["look_orientation"] = "Suunta",
        ["look_horizontal"] = "Vaakasuunnassa",
        ["look_vertical"] = "Pystysuunnassa",
        ["look_background"] = "Tausta",
        ["look_bg_glass"] = "Tumma lasi (läpikuultava)",
        ["look_bg_solid"] = "Kiinteä tumma",
        ["look_bg_none"] = "Ei taustaa, vain ruudut",
        ["look_size"] = "Koko",
        ["look_opacity"] = "Taustan peittävyys",
        ["look_disk"] = "Levyn prosentti",
        ["look_disk_used"] = "Käytetty tila",
        ["look_disk_activity"] = "Aktiivisuus (kuinka kiireinen)",
        ["look_accent"] = "Korostusväri",
        ["look_opacity_hint"] = "Taustan peittävyys säätää lasitaustaa tai, kun taustaa ei ole, ruutujen omaa taustaa. Kiinteä tausta on aina täysin peittävä.",
        ["look_show_labels"] = "Näytä otsikot ja kuvakkeet",
        ["look_show_usage"] = "Näytä käyttöprosentit ja palkit",

        // What to show
        ["show_title"] = "Näytä mittarissa",
        ["show_cpu"] = "Prosessori (CPU)",
        ["show_gpu"] = "Näytönohjain (GPU)",
        ["show_vram"] = "VRAM (näytönohjaimen muisti)",
        ["show_ram"] = "Muisti (RAM)",
        ["show_disks"] = "Levyt",

        // Position
        ["pos_title"] = "Sijainti",
        ["pos_on_top"] = "Näytä pelien ja muiden ikkunoiden päällä",
        ["pos_on_top_hint"] = "Kun tämä on pois päältä, mittari on tavallinen ikkuna, jonka edessä oleva peli peittää.",
        ["pos_top_left"] = "↖ Vasen ylä",
        ["pos_top_right"] = "↗ Oikea ylä",
        ["pos_bottom_left"] = "↙ Vasen ala",
        ["pos_bottom_right"] = "↘ Oikea ala",
        ["pos_edit"] = "Siirtotila päälle / pois",
        ["pos_hint"] = "Siirtotilassa mittarin voi raahata hiirellä minne tahansa, ja sen alle ilmestyvät painikkeet suunnan vaihtoon ja asetuksiin. Pikanäppäimet toimivat myös pelin aikana: Ctrl+Alt+F9 näyttää tai piilottaa mittarin, Ctrl+Alt+F10 avaa siirtotilan, Ctrl+Alt+F11 vaihtaa vaaka- ja pystyasettelun ja Ctrl+Alt+F12 avaa nämä asetukset. Joillakin näppäimistöillä pikanäppäimet toimivat sellaisenaan, toisilla pitää painaa myös Fn (Ctrl+Alt+Fn+F9). Pelin aikana mittari päästää kaikki klikkaukset läpi.",

        // Alert limits
        ["alert_title"] = "Hälytysrajat",
        ["alert_intro"] = "Lukema muuttuu keltaiseksi, kun lämpö lähestyy rajaa, ja punaiseksi sykkiväksi, kun raja ylittyy. Oletukset sopivat useimmille koneille.",
        ["alert_device_limits"] = "Käytä levyjen ja muistin omia rajoja, kun laite kertoo ne (suositus)",
        ["alert_device_hint"] = "Silloin RAM- ja levyrivien liukusäätimet ovat vain varalla, jos laite ei kerro omaa rajaansa.",
        ["alert_yellow"] = "Keltainen alkaa",
        ["alert_red"] = "Punainen alkaa",
        ["alert_reset"] = "Palauta oletusrajat",

        // Status
        ["status_title"] = "Tila",
        ["status_diag"] = "Tallenna diagnostiikka",
        ["status_demo"] = "Testaa hälytysvärit",
        ["status_admin_ok"] = "Oikeudet: järjestelmänvalvoja ✓",
        ["status_admin_missing"] = "Oikeudet: tavalliset. CPU- ja levylämmöt puuttuvat, käynnistä ohjelma järjestelmänvalvojana.",
        ["status_driver_ok"] = "Ajuri (CPU- ja muistilämmöt): asennettu ✓",
        ["status_driver_missing"] = "Ajuri (CPU- ja muistilämmöt): puuttuu. Asenna ohjelma asennuspaketista, niin ajuri tulee mukaan.",
        ["status_no_temp"] = "lämpöä ei saatavilla",

        // Tray icon
        ["tray_show"] = "Näytä mittari",
        ["tray_edit"] = "Siirtotila",
        ["tray_orientation"] = "Vaihda vaaka/pysty",
        ["tray_settings"] = "Asetukset…",
        ["tray_exit"] = "Sulje",
        ["tray_hint_title"] = "FrameHeat on käynnissä",
        ["tray_hint_text"] = "Asetukset: klikkaa tätä kuvaketta tai paina Ctrl+Alt+F12.\nSiirto ja suunta: Ctrl+Alt+F10.",

        // Messages
        ["msg_autostart_failed"] = "Käynnistystä Windowsin mukana ei voitu muuttaa. Se vaatii järjestelmänvalvojan oikeudet.",
        ["msg_autostart_ask"] = "Käynnistetäänkö FrameHeat automaattisesti, kun Windows käynnistyy?\n\nAsetuksen voi muuttaa myöhemmin kuvakkeen valikon Asetuksista.",

        // Dialog buttons
        ["button_yes"] = "Kyllä",
        ["button_no"] = "Ei",
        ["button_ok"] = "OK",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – diagnostiikka",
        ["diag_os"] = "Käyttöjärjestelmä",
        ["diag_selected"] = "Ruutuihin valitut mittarit:",
        ["diag_none"] = "(ei vielä mittauksia)",
        ["diag_all"] = "Kaikki löydetyt laitteet ja anturit:",
        ["diag_temp"] = "lämpö",
        ["diag_usage"] = "käyttö",

        // Frame rate and more appearance options
        ["fps_low"] = "1 % low",
        ["fps_display"] = "Näyttö",
        ["look_design_textrow"] = "Tekstirivi (yksi rivi laitetta kohti)",
        ["look_unit"] = "Lämpötilan yksikkö",
        ["look_unit_celsius"] = "Celsius (°C)",
        ["look_unit_fahrenheit"] = "Fahrenheit (°F)",
        ["look_accent_custom"] = "Muu väri…",
        ["shape_title"] = "Värit ja muoto",
        ["shape_warning"] = "Varoitusväri",
        ["shape_critical"] = "Kriittinen väri",
        ["shape_colors_reset"] = "Palauta oletusvärit",
        ["shape_corner"] = "Kulmien pyöristys",
        ["shape_spacing"] = "Ruutujen väli",
        ["shape_text"] = "Tekstin koko",
        ["shape_widget_opacity"] = "Mittarin peittävyys",
        ["show_fps"] = "FPS (pelin kuvataajuus)",
        ["show_fps_hint"] = "Kuvataajuus luetaan etualalla olevasta pelistä koskematta itse peliin. Se toimii DirectX 10, 11 ja 12 -peleissä ja vaatii järjestelmänvalvojan oikeudet.",
        ["show_order_hint"] = "Nuolilla vaihdat ruutujen järjestystä.",
        ["show_move_up"] = "Siirrä ylös",
        ["show_move_down"] = "Siirrä alas",
        ["show_disks_list"] = "Näytettävät levyt",
        ["show_disks_off_hint"] = "Ota yllä oleva Levyt käyttöön, niin voit valita, mitkä levyt näytetään.",
        ["status_fps_ok"] = "Kuvataajuus (FPS): käytettävissä ✓",
        ["status_fps_missing"] = "Kuvataajuus (FPS): ei käytettävissä. Se vaatii järjestelmänvalvojan oikeudet.",
    };
}
