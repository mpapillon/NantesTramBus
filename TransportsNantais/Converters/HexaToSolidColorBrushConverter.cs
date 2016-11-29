using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace TransportsNantais.Converters
{
    public class HexaToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty((string)value))
            {
                return DependencyProperty.UnsetValue;
            }
            var color = (string)value;

            var colorBrush = new SolidColorBrush(Color.FromArgb(255,
                byte.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                byte.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                byte.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber)));

            return colorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var brush = (SolidColorBrush)value;
            return brush.Color.ToString();
        }
    }
}
