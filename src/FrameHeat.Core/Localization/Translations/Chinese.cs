namespace FrameHeat.Core.Localization.Translations;

internal static class Chinese
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "正在查找传感器…",
        ["overlay_edit_hint"] = "将小组件拖到合适的位置",
        ["overlay_btn_orientation"] = "切换方向",
        ["overlay_btn_settings"] = "设置",
        ["overlay_btn_done"] = "完成",
        ["metric_disk"] = "磁盘",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – 设置",
        ["settings_intro"] = "小组件的外观、警报阈值和位置设置。",

        // Startup
        ["startup_title"] = "启动",
        ["startup_autostart"] = "随 Windows 启动",
        ["startup_hint"] = "这样 Windows 启动时小组件会自动出现，无需手动打开。",

        // Appearance
        ["look_title"] = "外观",
        ["look_design"] = "样式",
        ["look_design_gauge"] = "环形仪表",
        ["look_design_compact"] = "紧凑（数字和进度条）",
        ["look_orientation"] = "方向",
        ["look_horizontal"] = "横向",
        ["look_vertical"] = "纵向",
        ["look_background"] = "背景",
        ["look_bg_glass"] = "深色玻璃（半透明）",
        ["look_bg_solid"] = "纯深色",
        ["look_bg_none"] = "无背景，仅显示磁贴",
        ["look_size"] = "大小",
        ["look_opacity"] = "背景不透明度",
        ["look_disk"] = "磁盘百分比",
        ["look_disk_used"] = "已用空间",
        ["look_disk_activity"] = "活动（繁忙程度）",
        ["look_accent"] = "强调色",
        ["look_opacity_hint"] = "不透明度用于调整玻璃背景；没有背景时，则调整磁贴自身的背景。纯色背景始终完全不透明。",
        ["look_show_labels"] = "显示标签和图标",
        ["look_show_usage"] = "显示使用百分比和进度条",

        // What to show
        ["show_title"] = "在小组件中显示",
        ["show_cpu"] = "处理器 (CPU)",
        ["show_gpu"] = "显卡 (GPU)",
        ["show_vram"] = "显存 (VRAM)",
        ["show_ram"] = "内存 (RAM)",
        ["show_disks"] = "磁盘",

        // Position
        ["pos_title"] = "位置",
        ["pos_on_top"] = "显示在游戏和其他窗口上方",
        ["pos_on_top_hint"] = "关闭后，小组件将成为普通窗口，位于其前方的游戏会将其遮住。",
        ["pos_top_left"] = "↖ 左上角",
        ["pos_top_right"] = "↗ 右上角",
        ["pos_bottom_left"] = "↙ 左下角",
        ["pos_bottom_right"] = "↘ 右下角",
        ["pos_edit"] = "移动模式 开 / 关",
        ["pos_hint"] = "在移动模式下，可以用鼠标将小组件拖到任意位置，其下方会出现用于切换方向和打开设置的按钮。快捷键在游戏中同样有效：Ctrl+Alt+F9 显示或隐藏小组件，Ctrl+Alt+F10 打开移动模式，Ctrl+Alt+F11 在横向和纵向布局之间切换，Ctrl+Alt+F12 打开此设置。在某些键盘上，快捷键可直接使用；在另一些键盘上，还需要同时按住 Fn（Ctrl+Alt+Fn+F9）。游戏过程中，小组件会让所有点击穿透。",

        // Alert limits
        ["alert_title"] = "警报阈值",
        ["alert_intro"] = "温度接近阈值时读数变为黄色，超过阈值后变为闪烁的红色。默认值适用于大多数电脑。",
        ["alert_device_limits"] = "设备提供自身阈值时，使用磁盘和内存自带的阈值（推荐）",
        ["alert_device_hint"] = "此时内存和磁盘的滑块仅作备用，用于设备未提供自身阈值的情况。",
        ["alert_yellow"] = "黄色起点",
        ["alert_red"] = "红色起点",
        ["alert_reset"] = "恢复默认阈值",

        // Status
        ["status_title"] = "状态",
        ["status_diag"] = "保存诊断信息",
        ["status_demo"] = "测试警报颜色",
        ["status_admin_ok"] = "权限：管理员 ✓",
        ["status_admin_missing"] = "权限：标准。无法获取 CPU 和磁盘温度，请以管理员身份运行程序。",
        ["status_driver_ok"] = "驱动程序（CPU 和内存温度）：已安装 ✓",
        ["status_driver_missing"] = "驱动程序（CPU 和内存温度）：缺失。请使用安装包安装本程序，驱动程序会一并安装。",
        ["status_no_temp"] = "温度不可用",

        // Tray icon
        ["tray_show"] = "显示小组件",
        ["tray_edit"] = "移动模式",
        ["tray_orientation"] = "切换横向/纵向",
        ["tray_settings"] = "设置…",
        ["tray_exit"] = "退出",
        ["tray_hint_title"] = "FrameHeat 正在运行",
        ["tray_hint_text"] = "设置：点击此图标或按 Ctrl+Alt+F12。\n移动和方向：Ctrl+Alt+F10。",

        // Messages
        ["msg_autostart_failed"] = "无法更改随 Windows 启动的设置，需要管理员权限。",
        ["msg_autostart_ask"] = "Windows 启动时是否自动启动 FrameHeat？\n\n以后可以在图标菜单的“设置”中更改。",

        // Dialog buttons
        ["button_yes"] = "是",
        ["button_no"] = "否",
        ["button_ok"] = "确定",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – 诊断",
        ["diag_os"] = "操作系统",
        ["diag_selected"] = "为磁贴选择的传感器：",
        ["diag_none"] = "（尚无测量数据）",
        ["diag_all"] = "找到的所有设备和传感器：",
        ["diag_temp"] = "温度",
        ["diag_usage"] = "使用率",

        // Frame rate and more appearance options
        ["fps_low"] = "1% Low",
        ["fps_display"] = "显示器",
        ["look_design_textrow"] = "文字行（每个设备一行）",
        ["look_unit"] = "温度单位",
        ["look_unit_celsius"] = "摄氏度 (°C)",
        ["look_unit_fahrenheit"] = "华氏度 (°F)",
        ["look_accent_custom"] = "其他颜色…",
        ["shape_title"] = "颜色和形状",
        ["shape_warning"] = "警告颜色",
        ["shape_critical"] = "严重颜色",
        ["shape_colors_reset"] = "恢复默认颜色",
        ["shape_corner"] = "圆角大小",
        ["shape_spacing"] = "磁贴间距",
        ["shape_text"] = "文字大小",
        ["shape_widget_opacity"] = "小组件不透明度",
        ["show_fps"] = "FPS（游戏帧率）",
        ["show_fps_hint"] = "帧率从前台游戏中读取，不会触碰游戏本身。适用于 DirectX 10、11 和 12 游戏，需要管理员权限。",
        ["show_order_hint"] = "用箭头调整磁贴的顺序。",
        ["show_move_up"] = "上移",
        ["show_move_down"] = "下移",
        ["show_disks_list"] = "显示这些磁盘",
        ["show_disks_off_hint"] = "请先开启上方的“磁盘”，再选择要显示的磁盘。",
        ["status_fps_ok"] = "帧率 (FPS)：可用 ✓",
        ["status_fps_missing"] = "帧率 (FPS)：不可用，需要管理员权限。",
    };
}
