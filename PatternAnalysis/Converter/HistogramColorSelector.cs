using OxyPlot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace PatternAnalysis.Converter
{
    public class HistogramColorSelector : IValueConverter
    {

        private readonly Color _bin50 = Colors.LightGreen;
        private readonly Color _bin100 = Colors.Green;
        private readonly Color _bin200 = Colors.DarkGreen;
        private readonly Color _bin300 = Colors.Orange;
        private readonly Color _bin500 = Colors.DarkOrange;
        private readonly Color _bin100000 = Colors.DarkRed;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                if ((double)value < 50) return new SolidColorBrush(_bin50);
                if ((double)value >= 50 && (double)value < 100) return new SolidColorBrush(_bin100);
                if ((double)value >= 100 && (double)value < 200) return new SolidColorBrush(_bin200);
                if ((double)value >= 200 && (double)value < 300) return new SolidColorBrush(_bin300);
                if ((double)value >= 300 && (double)value < 500) return new SolidColorBrush(_bin500);
                else return new SolidColorBrush(_bin100000);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
