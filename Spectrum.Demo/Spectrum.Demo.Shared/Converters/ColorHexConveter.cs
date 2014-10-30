using System;
using Windows.UI.Xaml.Data;
using Spectrum.Universal;

namespace Spectrum.Demo.Converters
{
    public class ColorHexConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = (Windows.UI.Color)value;

            var rgb = color.FromSystemColor();

            return rgb.ToHexString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
