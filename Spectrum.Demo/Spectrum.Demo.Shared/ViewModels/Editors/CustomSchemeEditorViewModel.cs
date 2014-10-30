using System;
using WinColor = Windows.UI.Color;

namespace Spectrum.Demo.ViewModels.Editors
{
    public class CustomSchemeEditorViewModel : SchemeEditorViewModelBase
    {
        public void Add(WinColor color)
        {
            Colors.Add(color);
        }

        public void Remove(WinColor color)
        {
            Colors.Remove(color);
        }
    }
}
