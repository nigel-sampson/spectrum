using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Spectrum.Universal.Converters
{
    public class ForegroundTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = (Windows.UI.Color) value;

            var hsl = color.FromSystemColor().ToHSL();

            var foreground = hsl.L < 0.5d ? Colors.White : Colors.Black;

            return new SolidColorBrush(foreground);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
