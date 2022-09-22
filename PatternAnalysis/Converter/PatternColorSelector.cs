using OxyPlot;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PatternAnalysis.Converter;

class PatternColorSelector : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is OxyColor @oxyColor)
        {
            var color = (Color)ColorConverter.ConvertFromString(@oxyColor.ToString());
            color.A = byte.MaxValue;
            return new SolidColorBrush(color);
        }
        return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
