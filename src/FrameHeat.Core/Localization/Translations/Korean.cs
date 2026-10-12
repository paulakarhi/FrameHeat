namespace FrameHeat.Core.Localization.Translations;

internal static class Korean
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "센서를 찾는 중…",
        ["overlay_edit_hint"] = "위젯을 원하는 위치로 끌어 놓으세요",
        ["overlay_btn_orientation"] = "방향 전환",
        ["overlay_btn_settings"] = "설정",
        ["overlay_btn_done"] = "완료",
        ["metric_disk"] = "디스크",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – 설정",
        ["settings_intro"] = "위젯의 모양, 경고 기준, 위치에 대한 설정입니다.",

        // Startup
        ["startup_title"] = "시작",
        ["startup_autostart"] = "Windows와 함께 시작",
        ["startup_hint"] = "Windows가 시작될 때 위젯이 자동으로 나타나므로 직접 열 필요가 없습니다.",

        // Appearance
        ["look_title"] = "모양",
        ["look_design"] = "스타일",
        ["look_design_gauge"] = "원형 게이지",
        ["look_design_compact"] = "간단히 (숫자와 막대)",
        ["look_orientation"] = "방향",
        ["look_horizontal"] = "가로",
        ["look_vertical"] = "세로",
        ["look_background"] = "배경",
        ["look_bg_glass"] = "어두운 유리 (반투명)",
        ["look_bg_solid"] = "어두운 단색",
        ["look_bg_none"] = "배경 없음, 타일만 표시",
        ["look_size"] = "크기",
        ["look_opacity"] = "배경 불투명도",
        ["look_disk"] = "디스크 퍼센트",
        ["look_disk_used"] = "사용 중인 공간",
        ["look_disk_activity"] = "활동량 (얼마나 바쁜지)",
        ["look_accent"] = "강조 색상",
        ["look_opacity_hint"] = "불투명도는 유리 배경을 조절하며, 배경이 없을 때는 타일 자체의 배경을 조절합니다. 어두운 단색 배경은 항상 완전히 불투명합니다.",
        ["look_show_labels"] = "라벨과 아이콘 표시",
        ["look_show_usage"] = "사용률 퍼센트와 막대 표시",

        // What to show
        ["show_title"] = "위젯에 표시할 항목",
        ["show_cpu"] = "프로세서 (CPU)",
        ["show_gpu"] = "그래픽 카드 (GPU)",
        ["show_vram"] = "VRAM (그래픽 카드 메모리)",
        ["show_ram"] = "메모리 (RAM)",
        ["show_disks"] = "디스크",

        // Position
        ["pos_title"] = "위치",
        ["pos_on_top"] = "게임 및 다른 창 위에 표시",
        ["pos_on_top_hint"] = "끄면 위젯이 일반 창이 되어, 앞에 있는 게임에 가려집니다.",
        ["pos_top_left"] = "↖ 왼쪽 위",
        ["pos_top_right"] = "↗ 오른쪽 위",
        ["pos_bottom_left"] = "↙ 왼쪽 아래",
        ["pos_bottom_right"] = "↘ 오른쪽 아래",
        ["pos_edit"] = "이동 모드 켜기 / 끄기",
        ["pos_hint"] = "이동 모드에서는 마우스로 위젯을 원하는 곳으로 끌 수 있으며, 아래에 방향 전환과 설정 열기 버튼이 나타납니다. 단축키는 게임 중에도 작동합니다. Ctrl+Alt+F9는 위젯을 표시하거나 숨기고, Ctrl+Alt+F10은 이동 모드를 열고, Ctrl+Alt+F11은 가로와 세로 배치를 전환하며, Ctrl+Alt+F12는 이 설정을 엽니다. 키보드에 따라 단축키가 그대로 작동하기도 하고, Fn 키를 함께 눌러야 하기도 합니다(Ctrl+Alt+Fn+F9). 게임 중에는 위젯이 모든 클릭을 통과시킵니다.",

        // Alert limits
        ["alert_title"] = "경고 기준값",
        ["alert_intro"] = "온도가 기준값에 가까워지면 노란색으로, 기준값을 넘으면 깜박이는 빨간색으로 바뀝니다. 기본값은 대부분의 컴퓨터에 적합합니다.",
        ["alert_device_limits"] = "장치가 자체 한계값을 알려 주면 디스크와 메모리의 자체 한계값 사용 (권장)",
        ["alert_device_hint"] = "이 경우 RAM과 디스크 슬라이더는 장치가 자체 한계값을 알려 주지 않을 때의 예비 값으로만 사용됩니다.",
        ["alert_yellow"] = "노란색 시작",
        ["alert_red"] = "빨간색 시작",
        ["alert_reset"] = "기본 기준값으로 복원",

        // Status
        ["status_title"] = "상태",
        ["status_diag"] = "진단 정보 저장",
        ["status_demo"] = "경고 색상 테스트",
        ["status_admin_ok"] = "권한: 관리자 ✓",
        ["status_admin_missing"] = "권한: 표준. CPU와 디스크 온도를 읽을 수 없습니다. 프로그램을 관리자 권한으로 실행하세요.",
        ["status_driver_ok"] = "드라이버 (CPU 및 메모리 온도): 설치됨 ✓",
        ["status_driver_missing"] = "드라이버 (CPU 및 메모리 온도): 없음. 설치 프로그램으로 설치하면 드라이버가 함께 설치됩니다.",
        ["status_no_temp"] = "온도를 사용할 수 없음",

        // Tray icon
        ["tray_show"] = "위젯 표시",
        ["tray_edit"] = "이동 모드",
        ["tray_orientation"] = "가로/세로 전환",
        ["tray_settings"] = "설정…",
        ["tray_exit"] = "종료",
        ["tray_hint_title"] = "FrameHeat가 실행 중입니다",
        ["tray_hint_text"] = "설정: 이 아이콘을 클릭하거나 Ctrl+Alt+F12를 누르세요.\n이동 및 방향: Ctrl+Alt+F10.",

        // Messages
        ["msg_autostart_failed"] = "Windows와 함께 시작하는 설정을 변경할 수 없습니다. 관리자 권한이 필요합니다.",
        ["msg_autostart_ask"] = "Windows가 시작될 때 FrameHeat를 자동으로 시작할까요?\n\n나중에 아이콘 메뉴의 '설정'에서 변경할 수 있습니다.",

        // Dialog buttons
        ["button_yes"] = "예",
        ["button_no"] = "아니요",
        ["button_ok"] = "확인",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – 진단",
        ["diag_os"] = "운영 체제",
        ["diag_selected"] = "타일에 선택된 센서:",
        ["diag_none"] = "(아직 측정값 없음)",
        ["diag_all"] = "발견된 모든 장치와 센서:",
        ["diag_temp"] = "온도",
        ["diag_usage"] = "사용률",

        // Frame rate and more appearance options
        ["fps_low"] = "1% 로우",
        ["fps_display"] = "모니터",
        ["look_design_textrow"] = "텍스트 줄 (장치당 한 줄)",
        ["look_unit"] = "온도 단위",
        ["look_unit_celsius"] = "섭씨 (°C)",
        ["look_unit_fahrenheit"] = "화씨 (°F)",
        ["look_accent_custom"] = "다른 색…",
        ["shape_title"] = "색상 및 모양",
        ["shape_warning"] = "경고 색상",
        ["shape_critical"] = "위험 색상",
        ["shape_colors_reset"] = "기본 색상으로 복원",
        ["shape_corner"] = "모서리 둥글기",
        ["shape_spacing"] = "타일 간격",
        ["shape_text"] = "글자 크기",
        ["shape_widget_opacity"] = "위젯 불투명도",
        ["show_fps"] = "FPS (게임 프레임 속도)",
        ["show_fps_hint"] = "프레임 속도는 게임 자체를 건드리지 않고 전면에 있는 게임에서 읽습니다. DirectX 10, 11, 12 게임에서 작동하며 관리자 권한이 필요합니다.",
        ["show_order_hint"] = "화살표로 타일 순서를 바꿉니다.",
        ["show_move_up"] = "위로 이동",
        ["show_move_down"] = "아래로 이동",
        ["show_disks_list"] = "표시할 디스크",
        ["show_disks_off_hint"] = "위의 ‘디스크’를 켜면 표시할 디스크를 선택할 수 있습니다.",
        ["status_fps_ok"] = "프레임 속도 (FPS): 사용 가능 ✓",
        ["status_fps_missing"] = "프레임 속도 (FPS): 사용할 수 없습니다. 관리자 권한이 필요합니다.",
    };
}
