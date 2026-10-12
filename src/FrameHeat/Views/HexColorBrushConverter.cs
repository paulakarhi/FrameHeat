using System.Globalization;
using System.Windows.Data;
using FrameHeat.ViewModels;

namespace FrameHeat.Views;

/// <summary>Shows a color setting such as "#FFB627" as a brush. The parameter is the color to use if the setting is not valid.</summary>
internal sealed class HexColorBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => Palette.UserColor(value as string, (string)parameter);

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
