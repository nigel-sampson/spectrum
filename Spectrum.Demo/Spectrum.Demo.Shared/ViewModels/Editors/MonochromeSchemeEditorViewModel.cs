using System;
using System.Collections.Generic;
using System.Linq;
using Spectrum.Demo.Services;
using Spectrum.Universal;
using WinColor = Windows.UI.Color;

namespace Spectrum.Demo.ViewModels.Editors
{
    public class MonochromeSchemeEditorViewModel : SingleColorSchemeEditorViewModelBase
    {
        public MonochromeSchemeEditorViewModel(IWindowManager windowManager)
            : base(windowManager)
        {
            
        }

        public override IEnumerable<Color.RGB> GetColours()
        {
            var rgb = CurrentColor.Color.FromSystemColor();
            var hsl = rgb.ToHSL();

            return new[]
            {
                hsl.ShiftLightness(-0.2).ToRGB(),
                hsl.ShiftLightness(-0.1).ToRGB(),
                rgb,
                hsl.ShiftLightness(0.1).ToRGB(),
                hsl.ShiftLightness(0.2).ToRGB(),
            };
        }
    }
}
