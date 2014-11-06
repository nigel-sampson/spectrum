using System;
using Caliburn.Micro;
using PropertyChanged;
using Spectrum.Universal;
using WinColor = Windows.UI.Color;

namespace Spectrum.Demo.ViewModels.Editors
{
    public class ColourViewModel : PropertyChangedBase
    {
        public WinColor Color
        {
            get; set;
        }

        [DependsOn("Color")]
        public string Description
        {
            get { return Color.FromSystemColor().ToHexString(); }
        }
    }
}
