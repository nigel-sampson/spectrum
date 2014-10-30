using System;
using Caliburn.Micro;
using WinColor = Windows.UI.Color;

namespace Spectrum.Demo.ViewModels.Editors
{
    public abstract class SchemeEditorViewModelBase : Screen
    {
        protected SchemeEditorViewModelBase()
        {
            Colors = new BindableCollection<WinColor>();
        }

        public BindableCollection<WinColor> Colors
        {
            get; private set;
        } 
    }
}
