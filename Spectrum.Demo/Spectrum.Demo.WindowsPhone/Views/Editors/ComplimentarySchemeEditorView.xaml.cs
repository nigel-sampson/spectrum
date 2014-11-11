using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Spectrum.Demo.Views.Editors
{
    public sealed partial class ComplimentarySchemeEditorView
    {
        public ComplimentarySchemeEditorView()
        {
            InitializeComponent();
        }

        protected override Panel GetInputPanel()
        {
            return InputPanel;
        }

        protected override Slider GetSaturationSlider()
        {
            return SaturationSlider;
        }

        protected override IEnumerable<IColourView> GetColourViews()
        {
            return new IColourView[]
            {
                MainColor,
                Color1,
                Color2,
                Color3,
                Color4
            };
        }
    }
}
