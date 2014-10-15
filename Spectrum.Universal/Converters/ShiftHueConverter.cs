using System;
using Windows.UI.Xaml.Data;

namespace Spectrum.Universal.Converters
{
    public class ShiftHueConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, string language)
        {
            parameter = parameter ?? "0.0";

            var shifyBy = Double.Parse(parameter.ToString());

            var color = (Windows.UI.Color)value;

            return color
                .FromSystemColor()
                .ToHSL()
                .ShiftHue(shifyBy)
                .ToRGB()
                .ToSystemColor(color.A);
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            parameter = parameter ?? "0.0";

            var shiftBy = Double.Parse(parameter.ToString());

            return Convert(value, targetType, -shiftBy, language);
        }
    }
}
