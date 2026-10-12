namespace FrameHeat.Core.Localization.Translations;

internal static class Spanish
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "Buscando sensores…",
        ["overlay_edit_hint"] = "Arrastra el widget a su sitio",
        ["overlay_btn_orientation"] = "Cambiar orientación",
        ["overlay_btn_settings"] = "Configuración",
        ["overlay_btn_done"] = "Listo",
        ["metric_disk"] = "Disco",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – Configuración",
        ["settings_intro"] = "Ajustes del aspecto, los límites de alerta y la posición del widget.",

        // Startup
        ["startup_title"] = "Inicio",
        ["startup_autostart"] = "Iniciar con Windows",
        ["startup_hint"] = "Así el widget aparece solo cuando Windows arranca, sin tener que abrirlo a mano.",

        // Appearance
        ["look_title"] = "Aspecto",
        ["look_design"] = "Estilo",
        ["look_design_gauge"] = "Indicadores circulares",
        ["look_design_compact"] = "Compacto (número y barra)",
        ["look_orientation"] = "Orientación",
        ["look_horizontal"] = "Horizontal",
        ["look_vertical"] = "Vertical",
        ["look_background"] = "Fondo",
        ["look_bg_glass"] = "Cristal oscuro (translúcido)",
        ["look_bg_solid"] = "Oscuro sólido",
        ["look_bg_none"] = "Sin fondo, solo las tarjetas",
        ["look_size"] = "Tamaño",
        ["look_opacity"] = "Opacidad del fondo",
        ["look_disk"] = "Porcentaje del disco",
        ["look_disk_used"] = "Espacio usado",
        ["look_disk_activity"] = "Actividad (cuán ocupado está)",
        ["look_accent"] = "Color de énfasis",
        ["look_opacity_hint"] = "La opacidad ajusta el fondo de cristal o, cuando no hay fondo, el fondo propio de las tarjetas. El fondo sólido siempre es totalmente opaco.",
        ["look_show_labels"] = "Mostrar etiquetas e iconos",
        ["look_show_usage"] = "Mostrar porcentajes de uso y barras",

        // What to show
        ["show_title"] = "Mostrar en el widget",
        ["show_cpu"] = "Procesador (CPU)",
        ["show_gpu"] = "Tarjeta gráfica (GPU)",
        ["show_vram"] = "VRAM (memoria de la tarjeta gráfica)",
        ["show_ram"] = "Memoria (RAM)",
        ["show_disks"] = "Discos",

        // Position
        ["pos_title"] = "Posición",
        ["pos_on_top"] = "Mostrar encima de los juegos y otras ventanas",
        ["pos_on_top_hint"] = "Si lo desactivas, el widget pasa a ser una ventana normal que un juego situado delante oculta.",
        ["pos_top_left"] = "↖ Arriba a la izquierda",
        ["pos_top_right"] = "↗ Arriba a la derecha",
        ["pos_bottom_left"] = "↙ Abajo a la izquierda",
        ["pos_bottom_right"] = "↘ Abajo a la derecha",
        ["pos_edit"] = "Modo de movimiento activado / desactivado",
        ["pos_hint"] = "En el modo de movimiento, el widget se puede arrastrar a cualquier lugar con el ratón, y debajo aparecen botones para cambiar la orientación y abrir la configuración. Los atajos de teclado también funcionan durante el juego: Ctrl+Alt+F9 muestra u oculta el widget, Ctrl+Alt+F10 abre el modo de movimiento, Ctrl+Alt+F11 alterna entre orientación horizontal y vertical, y Ctrl+Alt+F12 abre esta configuración. En algunos teclados los atajos funcionan tal cual; en otros hay que mantener pulsada también la tecla Fn (Ctrl+Alt+Fn+F9). Durante el juego, el widget deja pasar todos los clics.",

        // Alert limits
        ["alert_title"] = "Límites de alerta",
        ["alert_intro"] = "El valor se vuelve amarillo cuando la temperatura se acerca al límite y rojo parpadeante cuando se supera. Los valores predeterminados sirven para la mayoría de los equipos.",
        ["alert_device_limits"] = "Usar los límites propios de los discos y la memoria cuando el dispositivo los indique (recomendado)",
        ["alert_device_hint"] = "Entonces los controles deslizantes de RAM y disco solo sirven de respaldo si el dispositivo no indica su propio límite.",
        ["alert_yellow"] = "Amarillo desde",
        ["alert_red"] = "Rojo desde",
        ["alert_reset"] = "Restablecer límites predeterminados",

        // Status
        ["status_title"] = "Estado",
        ["status_diag"] = "Guardar diagnóstico",
        ["status_demo"] = "Probar colores de alerta",
        ["status_admin_ok"] = "Permisos: administrador ✓",
        ["status_admin_missing"] = "Permisos: estándar. Faltan las temperaturas de CPU y discos; ejecuta el programa como administrador.",
        ["status_driver_ok"] = "Controlador (temperaturas de CPU y memoria): instalado ✓",
        ["status_driver_missing"] = "Controlador (temperaturas de CPU y memoria): falta. Instala el programa con su instalador y el controlador se incluye.",
        ["status_no_temp"] = "temperatura no disponible",

        // Tray icon
        ["tray_show"] = "Mostrar widget",
        ["tray_edit"] = "Modo de movimiento",
        ["tray_orientation"] = "Cambiar horizontal/vertical",
        ["tray_settings"] = "Configuración…",
        ["tray_exit"] = "Salir",
        ["tray_hint_title"] = "FrameHeat se está ejecutando",
        ["tray_hint_text"] = "Configuración: haz clic en este icono o pulsa Ctrl+Alt+F12.\nMovimiento y orientación: Ctrl+Alt+F10.",

        // Messages
        ["msg_autostart_failed"] = "No se pudo cambiar el inicio con Windows. Se necesitan permisos de administrador.",
        ["msg_autostart_ask"] = "¿Quieres que FrameHeat se inicie automáticamente cuando arranque Windows?\n\nPuedes cambiarlo más adelante en Configuración, desde el menú del icono.",

        // Dialog buttons
        ["button_yes"] = "Sí",
        ["button_no"] = "No",
        ["button_ok"] = "Aceptar",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – diagnóstico",
        ["diag_os"] = "Sistema operativo",
        ["diag_selected"] = "Sensores elegidos para las tarjetas:",
        ["diag_none"] = "(aún no hay mediciones)",
        ["diag_all"] = "Todos los dispositivos y sensores encontrados:",
        ["diag_temp"] = "temp.",
        ["diag_usage"] = "uso",

        // Frame rate and more appearance options
        ["fps_low"] = "1 % low",
        ["fps_display"] = "Pantalla",
        ["look_design_textrow"] = "Texto, una línea por equipo",
        ["look_unit"] = "Unidad de temperatura",
        ["look_unit_celsius"] = "Celsius (°C)",
        ["look_unit_fahrenheit"] = "Fahrenheit (°F)",
        ["look_accent_custom"] = "Otro color…",
        ["shape_title"] = "Colores y forma",
        ["shape_warning"] = "Color de advertencia",
        ["shape_critical"] = "Color crítico",
        ["shape_colors_reset"] = "Restaurar colores predeterminados",
        ["shape_corner"] = "Redondeo de esquinas",
        ["shape_spacing"] = "Espacio entre tarjetas",
        ["shape_text"] = "Tamaño del texto",
        ["shape_widget_opacity"] = "Opacidad del widget",
        ["show_fps"] = "FPS (tasa de fotogramas del juego)",
        ["show_fps_hint"] = "La tasa de fotogramas se lee del juego en primer plano sin tocar el juego. Funciona con juegos DirectX 10, 11 y 12 y requiere permisos de administrador.",
        ["show_order_hint"] = "Las flechas cambian el orden de las tarjetas.",
        ["show_move_up"] = "Subir",
        ["show_move_down"] = "Bajar",
        ["show_disks_list"] = "Discos que se muestran",
        ["show_disks_off_hint"] = "Activa «Discos» arriba para elegir qué discos se muestran.",
        ["status_fps_ok"] = "Tasa de fotogramas (FPS): disponible ✓",
        ["status_fps_missing"] = "Tasa de fotogramas (FPS): no disponible. Requiere permisos de administrador.",
    };
}
