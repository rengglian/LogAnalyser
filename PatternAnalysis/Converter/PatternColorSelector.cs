using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PatternAnalysis.Converter
{
    class PatternColorSelector : IValueConverter
    {

        private readonly Color _pulseRaw = Colors.Chocolate;
        private readonly Color _pulseList = Colors.Coral;
        private readonly Color _pulsePosition = Colors.CornflowerBlue;
        private readonly Color _checkerPosition = Colors.SlateBlue;
        private readonly Color _anythingElse = Colors.Gray;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string @string)
            {
                if (@string.Contains("PulseList-Raw")) return new SolidColorBrush(_pulseRaw);
                if (@string.Contains("patternPositions") || @string.Contains("PulseList-Edited")) return new SolidColorBrush(_pulseList);
                if (@string.Contains("pulsePosition") || @string.Contains("PulsePosition")) return new SolidColorBrush(_pulsePosition);
                if (@string.Contains("checkerPosition") || @string.Contains("CheckerPosition")) return new SolidColorBrush(_checkerPosition);
                else return new SolidColorBrush(_anythingElse);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
