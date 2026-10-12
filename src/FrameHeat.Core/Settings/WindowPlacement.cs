namespace FrameHeat.Core.Settings;

/// <summary>Where the widget was left, in screen units. Both values are empty until the widget has been placed once.</summary>
public sealed class WindowPlacement : ObservableObject
{
    private double? _left;
    private double? _top;

    public double? Left { get => _left; set => Set(ref _left, value); }
    public double? Top { get => _top; set => Set(ref _top, value); }
}
