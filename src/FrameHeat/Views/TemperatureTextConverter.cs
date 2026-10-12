using System.Globalization;
using System.Windows.Data;

namespace FrameHeat.Views;

/// <summary>Writes a Celsius value in the unit the user has chosen, for example "86 °F". Inputs: the value and the unit.</summary>
internal sealed class TemperatureTextConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        => values is [double celsius, TemperatureUnit unit] ? Temperature.Format(celsius, unit) : "";

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
