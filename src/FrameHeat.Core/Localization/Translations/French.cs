namespace FrameHeat.Core.Localization.Translations;

internal static class French
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "Recherche des capteurs…",
        ["overlay_edit_hint"] = "Faites glisser le widget à l'endroit voulu",
        ["overlay_btn_orientation"] = "Changer l'orientation",
        ["overlay_btn_settings"] = "Paramètres",
        ["overlay_btn_done"] = "Terminé",
        ["metric_disk"] = "Disque",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – Paramètres",
        ["settings_intro"] = "Paramètres de l'apparence, des seuils d'alerte et de la position du widget.",

        // Startup
        ["startup_title"] = "Démarrage",
        ["startup_autostart"] = "Démarrer avec Windows",
        ["startup_hint"] = "Le widget s'affiche alors tout seul au démarrage de Windows, sans qu'il soit nécessaire de l'ouvrir à la main.",

        // Appearance
        ["look_title"] = "Apparence",
        ["look_design"] = "Style",
        ["look_design_gauge"] = "Jauges circulaires",
        ["look_design_compact"] = "Compact (chiffre et barre)",
        ["look_orientation"] = "Orientation",
        ["look_horizontal"] = "Horizontale",
        ["look_vertical"] = "Verticale",
        ["look_background"] = "Arrière-plan",
        ["look_bg_glass"] = "Verre sombre (translucide)",
        ["look_bg_solid"] = "Sombre uni",
        ["look_bg_none"] = "Sans fond, vignettes seules",
        ["look_size"] = "Taille",
        ["look_opacity"] = "Opacité de l'arrière-plan",
        ["look_disk"] = "Pourcentage du disque",
        ["look_disk_used"] = "Espace utilisé",
        ["look_disk_activity"] = "Activité (niveau d'occupation)",
        ["look_accent"] = "Couleur d'accentuation",
        ["look_opacity_hint"] = "L'opacité règle l'arrière-plan en verre ou, en l'absence d'arrière-plan, le fond propre des vignettes. L'arrière-plan uni est toujours totalement opaque.",
        ["look_show_boxes"] = "Afficher un cadre autour de chaque vignette",
        ["look_show_labels"] = "Afficher les libellés et les icônes",
        ["look_show_usage"] = "Afficher les pourcentages d'utilisation et les barres",

        // What to show
        ["show_title"] = "Afficher dans le widget",
        ["show_cpu"] = "Processeur (CPU)",
        ["show_gpu"] = "Carte graphique (GPU)",
        ["show_vram"] = "VRAM (mémoire de la carte graphique)",
        ["show_ram"] = "Mémoire (RAM)",
        ["show_disks"] = "Disques",

        // Position
        ["pos_title"] = "Position",
        ["pos_on_top"] = "Afficher au-dessus des jeux et des autres fenêtres",
        ["pos_on_top_hint"] = "Désactivé, le widget devient une fenêtre ordinaire qu'un jeu placé devant lui masque.",
        ["pos_top_left"] = "↖ En haut à gauche",
        ["pos_top_right"] = "↗ En haut à droite",
        ["pos_bottom_left"] = "↙ En bas à gauche",
        ["pos_bottom_right"] = "↘ En bas à droite",
        ["pos_edit"] = "Mode déplacement activé / désactivé",
        ["pos_hint"] = "En mode déplacement, le widget peut être glissé n'importe où avec la souris ; des boutons pour changer l'orientation et ouvrir les paramètres apparaissent en dessous. Les raccourcis fonctionnent aussi en jeu : Ctrl+Alt+F9 affiche ou masque le widget, Ctrl+Alt+F10 ouvre le mode déplacement, Ctrl+Alt+F11 bascule entre l'orientation horizontale et verticale, et Ctrl+Alt+F12 ouvre ces paramètres. Sur certains claviers, les raccourcis fonctionnent tels quels ; sur d'autres, il faut aussi maintenir Fn (Ctrl+Alt+Fn+F9). En jeu, le widget laisse passer tous les clics.",

        // Alert limits
        ["alert_title"] = "Seuils d'alerte",
        ["alert_intro"] = "La valeur devient jaune lorsque la température approche du seuil, puis rouge clignotant une fois le seuil dépassé. Les valeurs par défaut conviennent à la plupart des ordinateurs.",
        ["alert_device_limits"] = "Utiliser les seuils propres aux disques et à la mémoire lorsque le matériel les indique (recommandé)",
        ["alert_device_hint"] = "Les curseurs RAM et disque ne servent alors que de solution de repli si le matériel n'indique pas son propre seuil.",
        ["alert_yellow"] = "Jaune à partir de",
        ["alert_red"] = "Rouge à partir de",
        ["alert_reset"] = "Rétablir les seuils par défaut",

        // Status
        ["status_title"] = "État",
        ["status_diag"] = "Enregistrer le diagnostic",
        ["status_demo"] = "Tester les couleurs d'alerte",
        ["status_admin_ok"] = "Autorisations : administrateur ✓",
        ["status_admin_missing"] = "Autorisations : standard. Les températures du CPU et des disques sont absentes ; lancez le programme en tant qu'administrateur.",
        ["status_driver_ok"] = "Pilote (températures du CPU et de la mémoire) : installé ✓",
        ["status_driver_missing"] = "Pilote (températures du CPU et de la mémoire) : absent. Installez le programme avec son programme d'installation, le pilote est inclus.",
        ["status_no_temp"] = "température indisponible",

        // Tray icon
        ["tray_show"] = "Afficher le widget",
        ["tray_edit"] = "Mode déplacement",
        ["tray_orientation"] = "Basculer horizontal/vertical",
        ["tray_settings"] = "Paramètres…",
        ["tray_exit"] = "Quitter",
        ["tray_hint_title"] = "FrameHeat est en cours d'exécution",
        ["tray_hint_text"] = "Paramètres : cliquez sur cette icône ou appuyez sur Ctrl+Alt+F12.\nDéplacement et orientation : Ctrl+Alt+F10.",

        // Messages
        ["msg_autostart_failed"] = "Impossible de modifier le démarrage avec Windows. Cela nécessite les droits d'administrateur.",
        ["msg_autostart_ask"] = "FrameHeat doit-il démarrer automatiquement au démarrage de Windows ?\n\nVous pourrez modifier ce choix plus tard dans les paramètres, via le menu de l'icône.",

        // Dialog buttons
        ["button_yes"] = "Oui",
        ["button_no"] = "Non",
        ["button_ok"] = "OK",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – diagnostic",
        ["diag_os"] = "Système d'exploitation",
        ["diag_selected"] = "Capteurs choisis pour les vignettes :",
        ["diag_none"] = "(aucune mesure pour l'instant)",
        ["diag_all"] = "Tous les appareils et capteurs trouvés :",
        ["diag_temp"] = "temp.",
        ["diag_usage"] = "utilisation",

        // Frame rate and more appearance options
        ["fps_low"] = "1 % low",
        ["fps_display"] = "Écran",
        ["look_design_textrow"] = "Ligne de texte (par appareil)",
        ["look_unit"] = "Unité de température",
        ["look_unit_celsius"] = "Celsius (°C)",
        ["look_unit_fahrenheit"] = "Fahrenheit (°F)",
        ["look_accent_custom"] = "Autre couleur…",
        ["shape_title"] = "Couleurs et forme",
        ["shape_warning"] = "Couleur d'avertissement",
        ["shape_critical"] = "Couleur critique",
        ["shape_colors_reset"] = "Rétablir les couleurs par défaut",
        ["shape_corner"] = "Arrondi des coins",
        ["shape_spacing"] = "Espace entre les vignettes",
        ["shape_text"] = "Taille du texte",
        ["shape_widget_opacity"] = "Opacité du widget",
        ["show_fps"] = "FPS (fréquence d'images du jeu)",
        ["show_fps_hint"] = "La fréquence d'images est lue sur le jeu au premier plan, sans toucher au jeu. Elle fonctionne avec les jeux DirectX 10, 11 et 12 et nécessite les droits d'administrateur.",
        ["show_order_hint"] = "Les flèches changent l'ordre des vignettes.",
        ["show_move_up"] = "Monter",
        ["show_move_down"] = "Descendre",
        ["show_disks_list"] = "Disques à afficher",
        ["show_disks_off_hint"] = "Activez « Disques » ci-dessus pour choisir les disques affichés.",
        ["status_fps_ok"] = "Fréquence d'images (FPS) : disponible ✓",
        ["status_fps_missing"] = "Fréquence d'images (FPS) : indisponible. Elle nécessite les droits d'administrateur.",
        ["show_net"] = "Débit réseau",
        ["show_ping"] = "Ping (délai vers une adresse)",
        ["show_ping_hint"] = "Le ping envoie un petit message de test une fois par seconde à l'adresse ci-dessus. Il ne fait rien tant que la vignette Ping n'est pas activée.",
        ["show_ping_address"] = "Adresse du ping",
        ["look_speed"] = "Unité de débit",
        ["look_speed_mbit"] = "Bits par seconde (Mbit/s)",
        ["look_speed_mbyte"] = "Octets par seconde (MB/s)",
        ["ping_loss"] = "Perdus",
        ["ping_average"] = "Moyenne",
        ["net_peak"] = "Maximum",
    };
}
