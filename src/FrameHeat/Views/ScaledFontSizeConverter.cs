using System.Globalization;
using System.Windows.Data;

namespace FrameHeat.Views;

/// <summary>Turns the text scale setting into a font size: the base size in the parameter times the scale.</summary>
internal sealed class ScaledFontSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var baseSize = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
        return value is double scale && double.IsFinite(scale) ? baseSize * scale : baseSize;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
