namespace FrameHeat.ViewModels;

/// <summary>One row of a "show in the widget" list: a switch that reads and writes a single visibility setting.</summary>
internal abstract class VisibilitySlot(Func<bool> isVisible, Action<bool> setVisible) : ObservableObject
{
    public bool IsVisible
    {
        get => isVisible();
        set
        {
            setVisible(value);
            OnPropertyChanged();
        }
    }

    /// <summary>Tells the list that the switch may have been changed somewhere else, for example by loading the settings.</summary>
    internal void VisibilityChangedElsewhere() => OnPropertyChanged(nameof(IsVisible));
}
