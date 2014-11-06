using System;
using System.Collections.Generic;
using Spectrum.Demo.Services;

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
            throw new NotImplementedException();
        }
    }
}
