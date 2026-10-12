using System.Windows;
using System.Windows.Controls;

namespace FrameHeat.Views;

/// <summary>One row of warning and critical sliders for a single metric.</summary>
internal sealed partial class AlertLimitRow : UserControl
{
    // The device name in front of the row; the sliders bind to the row's DataContext (its AlertLimits).
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
        nameof(Label), typeof(string), typeof(AlertLimitRow), new PropertyMetadata(""));

    public AlertLimitRow() => InitializeComponent();

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
}
