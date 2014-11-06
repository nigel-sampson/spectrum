using System;
using Windows.UI.Xaml.Input;

namespace Spectrum.Demo.Views.Editors
{
    public sealed partial class DerivedColourView
    {
        public DerivedColourView()
        {
            InitializeComponent();
        }

        private void OnHandlePointerMoved(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
