using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Spectrum.Universal;
using WinColor = Windows.UI.Color;

namespace Spectrum.Demo.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = Colors.Transparent;

            var rgb = value as Color.RGB;

            if (rgb != null)
            {
                color = rgb.ToSystemColor(255);
            }
            else if (value is WinColor)
            {
                color = (WinColor) value;
            }


            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
