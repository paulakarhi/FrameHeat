using FrameHeat.Layout;

namespace FrameHeat.Commands;

/// <summary>What the tray icon and the settings window can ask the application to do.</summary>
internal interface IAppCommands
{
    AppSettings Settings { get; }

    SensorSnapshot? LatestSnapshot { get; }

    /// <summary>What the widget shows right now: the sensors, or the made-up readings while the demo runs.</summary>
    SensorSnapshot? WidgetSnapshot { get; }

    // Current state, shown as check marks in the tray menu.
    bool IsVisible { get; }

    bool IsEditMode { get; }

    bool IsDemoRunning { get; }

    void ToggleVisible();

    void ToggleEditMode();

    void ToggleOrientation();

    void ToggleDemo();

    void ToggleAutoStart();

    void ShowSettings();

    void SnapTo(ScreenCorner corner);

    void SaveDiagnostics();

    void Exit();
}
