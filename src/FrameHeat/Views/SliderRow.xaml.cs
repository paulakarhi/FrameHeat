using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace FrameHeat.Views;

/// <summary>A labeled slider that shows its value as text, for the settings window.</summary>
internal sealed partial class SliderRow : UserControl
{
    public static readonly DependencyProperty LabelProperty = Register(nameof(Label), "");
    public static readonly DependencyProperty MinimumProperty = Register(nameof(Minimum), 0.0);
    public static readonly DependencyProperty MaximumProperty = Register(nameof(Maximum), 1.0);
    public static readonly DependencyProperty TickFrequencyProperty = Register(nameof(TickFrequency), 0.05);
    public static readonly DependencyProperty ValueProperty = Register(nameof(Value), 0.0, (row, _) => row.ShowValue(), twoWay: true);
    public static readonly DependencyProperty ValueFormatProperty = Register(nameof(ValueFormat), "{0:P0}", (row, _) => row.ShowValue());

    private static readonly DependencyPropertyKey ValueTextKey = DependencyProperty.RegisterReadOnly(
        nameof(ValueText), typeof(string), typeof(SliderRow), new PropertyMetadata(""));

    public static readonly DependencyProperty ValueTextProperty = ValueTextKey.DependencyProperty;

    public SliderRow()
    {
        InitializeComponent();
        ShowValue();
    }

    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    public double Minimum { get => (double)GetValue(MinimumProperty); set => SetValue(MinimumProperty, value); }
    public double Maximum { get => (double)GetValue(MaximumProperty); set => SetValue(MaximumProperty, value); }
    public double TickFrequency { get => (double)GetValue(TickFrequencyProperty); set => SetValue(TickFrequencyProperty, value); }
    public double Value { get => (double)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

    /// <summary>How the value is written next to the slider, as a format string such as "{0:P0}" or "{0:0}".</summary>
    public string ValueFormat { get => (string)GetValue(ValueFormatProperty); set => SetValue(ValueFormatProperty, value); }

    public string ValueText => (string)GetValue(ValueTextProperty);

    private void ShowValue() => SetValue(ValueTextKey, string.Format(CultureInfo.CurrentCulture, ValueFormat, Value));

    private static DependencyProperty Register<T>(string name, T defaultValue, Action<SliderRow, T>? changed = null, bool twoWay = false)
        => DependencyProperty.Register(name, typeof(T), typeof(SliderRow), new FrameworkPropertyMetadata(
            defaultValue,
            twoWay ? FrameworkPropertyMetadataOptions.BindsTwoWayByDefault : FrameworkPropertyMetadataOptions.None,
            (target, args) => changed?.Invoke((SliderRow)target, (T)args.NewValue)));
}
