namespace FrameHeat.Core.Localization.Translations;

internal static class Japanese
{
    public static readonly Dictionary<string, string> Strings = new()
    {
        // Widget
        ["overlay_searching"] = "センサーを検索しています…",
        ["overlay_edit_hint"] = "ウィジェットを好きな位置にドラッグしてください",
        ["overlay_btn_orientation"] = "向きを切り替え",
        ["overlay_btn_settings"] = "設定",
        ["overlay_btn_done"] = "完了",
        ["metric_disk"] = "ディスク",

        // Settings: general
        ["settings_window_title"] = "FrameHeat – 設定",
        ["settings_intro"] = "ウィジェットの外観、警告のしきい値、位置の設定です。",

        // Startup
        ["startup_title"] = "起動",
        ["startup_autostart"] = "Windows と同時に起動",
        ["startup_hint"] = "Windows の起動時にウィジェットが自動的に表示され、手動で開く必要がなくなります。",

        // Appearance
        ["look_title"] = "外観",
        ["look_design"] = "スタイル",
        ["look_design_gauge"] = "リングゲージ",
        ["look_design_compact"] = "コンパクト（数値とバー）",
        ["look_orientation"] = "向き",
        ["look_horizontal"] = "横向き",
        ["look_vertical"] = "縦向き",
        ["look_background"] = "背景",
        ["look_bg_glass"] = "ダークガラス（半透明）",
        ["look_bg_solid"] = "ダーク（不透明）",
        ["look_bg_none"] = "背景なし（タイルのみ）",
        ["look_size"] = "サイズ",
        ["look_opacity"] = "背景の不透明度",
        ["look_disk"] = "ディスクのパーセント表示",
        ["look_disk_used"] = "使用済み容量",
        ["look_disk_activity"] = "アクティビティ（負荷の高さ）",
        ["look_accent"] = "アクセントカラー",
        ["look_opacity_hint"] = "不透明度はガラス背景を調整します。背景がない場合は、タイル自体の背景を調整します。不透明の背景は常に完全に不透明です。",
        ["look_show_labels"] = "ラベルとアイコンを表示",
        ["look_show_usage"] = "使用率のパーセントとバーを表示",

        // What to show
        ["show_title"] = "ウィジェットに表示する項目",
        ["show_cpu"] = "プロセッサ (CPU)",
        ["show_gpu"] = "グラフィックスカード (GPU)",
        ["show_vram"] = "VRAM（グラフィックスカードのメモリ）",
        ["show_ram"] = "メモリ (RAM)",
        ["show_disks"] = "ディスク",

        // Position
        ["pos_title"] = "位置",
        ["pos_on_top"] = "ゲームやほかのウィンドウの上に表示する",
        ["pos_on_top_hint"] = "オフにすると、ウィジェットは通常のウィンドウになり、手前にあるゲームに隠れます。",
        ["pos_top_left"] = "↖ 左上",
        ["pos_top_right"] = "↗ 右上",
        ["pos_bottom_left"] = "↙ 左下",
        ["pos_bottom_right"] = "↘ 右下",
        ["pos_edit"] = "移動モード オン / オフ",
        ["pos_hint"] = "移動モードでは、ウィジェットをマウスで好きな場所にドラッグでき、その下に向きの切り替えと設定を開くボタンが表示されます。ショートカットキーはゲーム中も使えます。Ctrl+Alt+F9 でウィジェットの表示／非表示、Ctrl+Alt+F10 で移動モード、Ctrl+Alt+F11 で横向きと縦向きの切り替え、Ctrl+Alt+F12 でこの設定を開きます。キーボードによっては、そのまま使える場合と、Fn キーも同時に押す必要がある場合があります（Ctrl+Alt+Fn+F9）。ゲーム中、ウィジェットはクリックをすべて通過させます。",

        // Alert limits
        ["alert_title"] = "警告のしきい値",
        ["alert_intro"] = "温度がしきい値に近づくと黄色になり、しきい値を超えると赤色で点滅します。初期値はほとんどのパソコンに適しています。",
        ["alert_device_limits"] = "デバイスが独自の上限値を通知する場合は、ディスクとメモリの上限値を使用する（推奨）",
        ["alert_device_hint"] = "この場合、RAM とディスクのスライダーは、デバイスが上限値を通知しないときの予備としてのみ使われます。",
        ["alert_yellow"] = "黄色の開始",
        ["alert_red"] = "赤色の開始",
        ["alert_reset"] = "初期のしきい値に戻す",

        // Status
        ["status_title"] = "状態",
        ["status_diag"] = "診断情報を保存",
        ["status_demo"] = "警告色をテスト",
        ["status_admin_ok"] = "権限：管理者 ✓",
        ["status_admin_missing"] = "権限：標準。CPU とディスクの温度を取得できません。プログラムを管理者として実行してください。",
        ["status_driver_ok"] = "ドライバー（CPU とメモリの温度）：インストール済み ✓",
        ["status_driver_missing"] = "ドライバー（CPU とメモリの温度）：見つかりません。インストーラーでプログラムをインストールすると、ドライバーも一緒にインストールされます。",
        ["status_no_temp"] = "温度を取得できません",

        // Tray icon
        ["tray_show"] = "ウィジェットを表示",
        ["tray_edit"] = "移動モード",
        ["tray_orientation"] = "横向き／縦向きを切り替え",
        ["tray_settings"] = "設定…",
        ["tray_exit"] = "終了",
        ["tray_hint_title"] = "FrameHeat は実行中です",
        ["tray_hint_text"] = "設定：このアイコンをクリックするか Ctrl+Alt+F12 を押します。\n移動と向き：Ctrl+Alt+F10。",

        // Messages
        ["msg_autostart_failed"] = "Windows と同時に起動する設定を変更できませんでした。管理者権限が必要です。",
        ["msg_autostart_ask"] = "Windows の起動時に FrameHeat を自動的に起動しますか？\n\n後でアイコンのメニューの［設定］から変更できます。",

        // Dialog buttons
        ["button_yes"] = "はい",
        ["button_no"] = "いいえ",
        ["button_ok"] = "OK",

        // Diagnostics report
        ["diag_title"] = "FrameHeat – 診断",
        ["diag_os"] = "オペレーティングシステム",
        ["diag_selected"] = "タイルに選ばれたセンサー：",
        ["diag_none"] = "（まだ測定値がありません）",
        ["diag_all"] = "見つかったすべてのデバイスとセンサー：",
        ["diag_temp"] = "温度",
        ["diag_usage"] = "使用率",

        // Frame rate and more appearance options
        ["fps_low"] = "1% Low",
        ["fps_display"] = "モニター",
        ["look_design_textrow"] = "テキスト行（デバイスごと）",
        ["look_unit"] = "温度の単位",
        ["look_unit_celsius"] = "摂氏 (°C)",
        ["look_unit_fahrenheit"] = "華氏 (°F)",
        ["look_accent_custom"] = "その他の色…",
        ["shape_title"] = "色と形",
        ["shape_warning"] = "警告の色",
        ["shape_critical"] = "危険の色",
        ["shape_colors_reset"] = "既定の色に戻す",
        ["shape_corner"] = "角の丸み",
        ["shape_spacing"] = "タイルの間隔",
        ["shape_text"] = "文字サイズ",
        ["shape_widget_opacity"] = "ウィジェットの不透明度",
        ["show_fps"] = "FPS（ゲームのフレームレート）",
        ["show_fps_hint"] = "フレームレートは、ゲーム本体に触れずに、前面にあるゲームから読み取ります。DirectX 10、11、12 のゲームで動作し、管理者権限が必要です。",
        ["show_order_hint"] = "矢印でタイルの順番を変えられます。",
        ["show_move_up"] = "上へ",
        ["show_move_down"] = "下へ",
        ["show_disks_list"] = "表示するディスク",
        ["show_disks_off_hint"] = "上の「ディスク」をオンにすると、表示するディスクを選べます。",
        ["status_fps_ok"] = "フレームレート (FPS)：利用可能 ✓",
        ["status_fps_missing"] = "フレームレート (FPS)：利用できません。管理者権限が必要です。",
    };
}
