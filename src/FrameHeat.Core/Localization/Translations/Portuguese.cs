namespace FrameHeat.Core.Localization.Translations;

internal static class Portuguese
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "Procurando sensores…",
        ["overlay_edit_hint"] = "Arraste o widget para o lugar desejado",
        ["overlay_btn_orientation"] = "Alterar orientação",
        ["overlay_btn_settings"] = "Configurações",
        ["overlay_btn_done"] = "Concluído",
        ["metric_disk"] = "Disco",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – Configurações",
        ["settings_intro"] = "Configurações da aparência, dos limites de alerta e da posição do widget.",

        // Startup
        ["startup_title"] = "Inicialização",
        ["startup_autostart"] = "Iniciar com o Windows",
        ["startup_hint"] = "Assim o widget aparece sozinho quando o Windows inicia, sem precisar abri-lo manualmente.",

        // Appearance
        ["look_title"] = "Aparência",
        ["look_design"] = "Estilo",
        ["look_design_gauge"] = "Medidores circulares",
        ["look_design_compact"] = "Compacto (número e barra)",
        ["look_orientation"] = "Orientação",
        ["look_horizontal"] = "Horizontal",
        ["look_vertical"] = "Vertical",
        ["look_background"] = "Plano de fundo",
        ["look_bg_glass"] = "Vidro escuro (translúcido)",
        ["look_bg_solid"] = "Escuro sólido",
        ["look_bg_none"] = "Sem fundo, apenas os cartões",
        ["look_size"] = "Tamanho",
        ["look_opacity"] = "Opacidade do fundo",
        ["look_disk"] = "Porcentagem do disco",
        ["look_disk_used"] = "Espaço usado",
        ["look_disk_activity"] = "Atividade (quão ocupado está)",
        ["look_accent"] = "Cor de destaque",
        ["look_opacity_hint"] = "A opacidade ajusta o fundo de vidro ou, quando não há fundo, o fundo próprio dos cartões. O fundo sólido é sempre totalmente opaco.",
        ["look_show_labels"] = "Mostrar rótulos e ícones",
        ["look_show_usage"] = "Mostrar porcentagens de uso e barras",

        // What to show
        ["show_title"] = "Mostrar no widget",
        ["show_cpu"] = "Processador (CPU)",
        ["show_gpu"] = "Placa de vídeo (GPU)",
        ["show_vram"] = "VRAM (memória da placa de vídeo)",
        ["show_ram"] = "Memória (RAM)",
        ["show_disks"] = "Discos",

        // Position
        ["pos_title"] = "Posição",
        ["pos_on_top"] = "Mostrar por cima de jogos e outras janelas",
        ["pos_on_top_hint"] = "Desativado, o widget passa a ser uma janela comum que um jogo à frente dele esconde.",
        ["pos_top_left"] = "↖ Superior esquerdo",
        ["pos_top_right"] = "↗ Superior direito",
        ["pos_bottom_left"] = "↙ Inferior esquerdo",
        ["pos_bottom_right"] = "↘ Inferior direito",
        ["pos_edit"] = "Modo de movimento ativado / desativado",
        ["pos_hint"] = "No modo de movimento, o widget pode ser arrastado para qualquer lugar com o mouse, e abaixo dele aparecem botões para alterar a orientação e abrir as configurações. Os atalhos também funcionam durante os jogos: Ctrl+Alt+F9 mostra ou oculta o widget, Ctrl+Alt+F10 abre o modo de movimento, Ctrl+Alt+F11 alterna entre a orientação horizontal e vertical e Ctrl+Alt+F12 abre estas configurações. Em alguns teclados os atalhos funcionam como estão; em outros é preciso segurar também a tecla Fn (Ctrl+Alt+Fn+F9). Durante um jogo, o widget deixa passar todos os cliques.",

        // Alert limits
        ["alert_title"] = "Limites de alerta",
        ["alert_intro"] = "O valor fica amarelo quando a temperatura se aproxima do limite e vermelho pulsante quando o limite é ultrapassado. Os valores padrão servem para a maioria dos computadores.",
        ["alert_device_limits"] = "Usar os limites próprios dos discos e da memória quando o dispositivo os informar (recomendado)",
        ["alert_device_hint"] = "Nesse caso, os controles deslizantes de RAM e disco servem apenas de reserva, caso o dispositivo não informe seu próprio limite.",
        ["alert_yellow"] = "Amarelo a partir de",
        ["alert_red"] = "Vermelho a partir de",
        ["alert_reset"] = "Restaurar limites padrão",

        // Status
        ["status_title"] = "Status",
        ["status_diag"] = "Salvar diagnóstico",
        ["status_demo"] = "Testar cores de alerta",
        ["status_admin_ok"] = "Permissões: administrador ✓",
        ["status_admin_missing"] = "Permissões: padrão. As temperaturas de CPU e discos não estão disponíveis; execute o programa como administrador.",
        ["status_driver_ok"] = "Driver (temperaturas de CPU e memória): instalado ✓",
        ["status_driver_missing"] = "Driver (temperaturas de CPU e memória): ausente. Instale o programa pelo instalador e o driver será incluído.",
        ["status_no_temp"] = "temperatura indisponível",

        // Tray icon
        ["tray_show"] = "Mostrar widget",
        ["tray_edit"] = "Modo de movimento",
        ["tray_orientation"] = "Alternar horizontal/vertical",
        ["tray_settings"] = "Configurações…",
        ["tray_exit"] = "Sair",
        ["tray_hint_title"] = "O FrameHeat está em execução",
        ["tray_hint_text"] = "Configurações: clique neste ícone ou pressione Ctrl+Alt+F12.\nMover e orientação: Ctrl+Alt+F10.",

        // Messages
        ["msg_autostart_failed"] = "Não foi possível alterar a inicialização com o Windows. É necessário ter permissões de administrador.",
        ["msg_autostart_ask"] = "O FrameHeat deve iniciar automaticamente quando o Windows iniciar?\n\nVocê pode alterar isso depois em Configurações, no menu do ícone.",

        // Dialog buttons
        ["button_yes"] = "Sim",
        ["button_no"] = "Não",
        ["button_ok"] = "OK",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – diagnóstico",
        ["diag_os"] = "Sistema operacional",
        ["diag_selected"] = "Sensores escolhidos para os cartões:",
        ["diag_none"] = "(ainda sem medições)",
        ["diag_all"] = "Todos os dispositivos e sensores encontrados:",
        ["diag_temp"] = "temp.",
        ["diag_usage"] = "uso",

        // Frame rate and more appearance options
        ["fps_low"] = "1 % low",
        ["fps_display"] = "Tela",
        ["look_design_textrow"] = "Texto, uma linha por item",
        ["look_unit"] = "Unidade de temperatura",
        ["look_unit_celsius"] = "Celsius (°C)",
        ["look_unit_fahrenheit"] = "Fahrenheit (°F)",
        ["look_accent_custom"] = "Outra cor…",
        ["shape_title"] = "Cores e forma",
        ["shape_warning"] = "Cor de aviso",
        ["shape_critical"] = "Cor crítica",
        ["shape_colors_reset"] = "Restaurar cores padrão",
        ["shape_corner"] = "Arredondamento dos cantos",
        ["shape_spacing"] = "Espaço entre os cartões",
        ["shape_text"] = "Tamanho do texto",
        ["shape_widget_opacity"] = "Opacidade do widget",
        ["show_fps"] = "FPS (taxa de quadros do jogo)",
        ["show_fps_hint"] = "A taxa de quadros é lida do jogo em primeiro plano, sem mexer no jogo. Funciona com jogos DirectX 10, 11 e 12 e exige permissões de administrador.",
        ["show_order_hint"] = "As setas alteram a ordem dos cartões.",
        ["show_move_up"] = "Mover para cima",
        ["show_move_down"] = "Mover para baixo",
        ["show_disks_list"] = "Discos a mostrar",
        ["show_disks_off_hint"] = "Ative “Discos” acima para escolher quais discos são mostrados.",
        ["status_fps_ok"] = "Taxa de quadros (FPS): disponível ✓",
        ["status_fps_missing"] = "Taxa de quadros (FPS): indisponível. Exige permissões de administrador.",
    };
}
