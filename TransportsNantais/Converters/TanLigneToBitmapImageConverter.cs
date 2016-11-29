using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TransportsNantais.Models.TAN;

namespace TransportsNantais.Converters
{
    public class TanLigneToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string NumLigne = value as string;

            Uri imgUri = new Uri("/Assets/Lines/" + NumLigne + ".gif", UriKind.Relative);
            BitmapImage img = new BitmapImage(imgUri);

            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
