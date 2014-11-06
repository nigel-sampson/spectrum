using System;
using System.Collections.Generic;
using System.Text;
using Spectrum.Demo.Services;
using Spectrum.Universal;

namespace Spectrum.Demo.ViewModels.Editors
{
    public class AnalogousSchemeEditorViewModel : SingleColorSchemeEditorViewModelBase
    {
        public AnalogousSchemeEditorViewModel(IWindowManager windowManager)
            : base(windowManager)
        {
            
        }

        public override IEnumerable<Color.RGB> GetColours()
        {
            var rgb = CurrentColor.Color.FromSystemColor();
            var hsl = rgb.ToHSL();

            return new[]
            {
                hsl.ShiftHue(-30).ToRGB(),
                hsl.ShiftHue(15).ToRGB(),
                rgb,
                hsl.ShiftHue(15).ToRGB(),
                hsl.ShiftHue(30).ToRGB(),
            };
        }
    }
}
