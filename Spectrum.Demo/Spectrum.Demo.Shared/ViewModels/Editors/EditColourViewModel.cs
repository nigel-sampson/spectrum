using System;
using Caliburn.Micro;
using PropertyChanged;

namespace Spectrum.Demo.ViewModels.Editors
{
    public class EditColourViewModel : Screen
    {
        public EditColourViewModel(Color.RGB colour)
        {
            var hsl = colour.ToHSL();

            Hue = hsl.H;
            Saturation = hsl.S;
            Lightness = hsl.L;
        }

        [DependsOn("Hue", "Saturation", "Lightness")]
        public Color.RGB Colour
        {
            get
            {
                return new Color.HSL(Hue, Saturation, Lightness).ToRGB();
            }
        }

        public double Hue
        {
            get; set;
        }

        public double Saturation
        {
            get; set;
        }

        public double Lightness
        {
            get; set;
        }
    }
}
