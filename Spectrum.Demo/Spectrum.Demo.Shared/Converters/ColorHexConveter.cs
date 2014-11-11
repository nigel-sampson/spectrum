using System;
using Windows.UI.Xaml.Data;

namespace Spectrum.Demo.Converters
{
    public class ColorHexConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = (Windows.UI.Color)value;

            return String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
