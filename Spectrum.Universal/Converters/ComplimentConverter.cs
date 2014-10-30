using System;

namespace Spectrum.Universal.Converters
{
    public class ComplimentConverter : ShiftHueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            return base.Convert(value, targetType, "180.0", language);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return base.ConvertBack(value, targetType, "180.0", language);
        }
    }
}
