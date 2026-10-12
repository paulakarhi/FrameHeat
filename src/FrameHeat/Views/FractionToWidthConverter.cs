using System.Globalization;
using System.Windows.Data;

namespace FrameHeat.Views;

/// <summary>Turns a 0-1 fraction and the track width into the fill width in pixels.</summary>
internal sealed class FractionToWidthConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2 || values[0] is not double fraction || values[1] is not double trackWidth)
            return 0.0;

        // NaN would otherwise become the width, which WPF reads as "automatic".
        if (!double.IsFinite(fraction) || !double.IsFinite(trackWidth))
            return 0.0;

        return Math.Max(0, trackWidth * Math.Clamp(fraction, 0, 1));
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
