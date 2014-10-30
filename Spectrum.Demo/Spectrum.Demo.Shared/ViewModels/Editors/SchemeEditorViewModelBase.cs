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
