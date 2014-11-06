using System;
using System.Collections.Generic;
using System.Text;
using Spectrum.Demo.Services;

namespace Spectrum.Demo.ViewModels.Editors
{
    public class TriadSchemeEditorViewModel : SingleColorSchemeEditorViewModelBase
    {
        public TriadSchemeEditorViewModel(IWindowManager windowManager)
            : base(windowManager)
        {
            
        }

        public override IEnumerable<Color.RGB> GetColours()
        {
            throw new NotImplementedException();
        }
    }
}
