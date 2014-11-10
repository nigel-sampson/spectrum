using System;
using System.Collections.Generic;
using Spectrum.Demo.Services;
using Spectrum.Universal;

namespace Spectrum.Demo.ViewModels.Editors
{
    public class ComplimentarySchemeEditorViewModel : SingleColorSchemeEditorViewModelBase
    {
        public ComplimentarySchemeEditorViewModel(IWindowManager windowManager)
            : base(windowManager)
        {
            
        }

        public override IEnumerable<Color.RGB> GetColours()
        {
            var rgb = CurrentColor.Color.FromSystemColor();
            var hsl = rgb.ToHSL();

            return new[]
            {
                hsl.ShiftHue(180).ToRGB(),
                hsl.ShiftHue(180).ShiftLightness(0.05).ToRGB(),
                rgb,
                hsl.ShiftLightness(0.05).ToRGB(),
                hsl.ShiftLightness(0.1).ToRGB(),
            };
        }
    }
}
