using System;
using System.Collections.Generic;
using System.Linq;
using Spectrum.Demo.Services;
using Spectrum.Universal;

namespace Spectrum.Demo.ViewModels.Editors
{
    public abstract class SingleColorSchemeEditorViewModelBase : SchemeEditorViewModelBase
    {
        protected SingleColorSchemeEditorViewModelBase(IWindowManager windowManager)
            : base(windowManager)
        {
            var random = new Random();

            var hue = random.Next(0, 360);

            var hsl = new Color.HSL(hue, 0.5d, 0.5d);

            CurrentColor = new ColourViewModel { Color =hsl.ToRGB().ToSystemColor(255) };

            CurrentHue = hsl.H;
            CurrentSaturation = hsl.S;
            CurrentLuminosity = hsl.L;
        }

        public double CurrentHue
        {
            get;
            set;
        }

        public double CurrentSaturation
        {
            get;
            set;
        }

        public double CurrentLuminosity
        {
            get;
            set;
        }

        public ColourViewModel CurrentColor
        {
            get;
            set;
        }

        public void UpdateColor()
        {
            CurrentColor.Color = new Color.HSL(CurrentHue, CurrentSaturation, CurrentLuminosity)
                .ToRGB()
                .ToSystemColor(255);
        }

        public override void SetColors(IEnumerable<Color.RGB> colours)
        {
            CurrentColor = new ColourViewModel { Color = colours.ElementAt(2).ToSystemColor(255) };
        }

        public override bool CanSave
        {
            get { return true; }
        }
    }
}
