using System;

namespace Spectrum.Universal
{
    public static class ColorExtensions
    {
        public static Color.RGB FromSystemColor(this Windows.UI.Color color)
        {
            return new Color.RGB(color.R, color.G, color.B);
        }

        public static Windows.UI.Color ToSystemColor(this Color.RGB color, byte a)
        {
            return Windows.UI.Color.FromArgb(a, color.R, color.G, color.B);
        }
    }
}
