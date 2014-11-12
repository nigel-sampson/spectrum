using System;
using Windows.UI.Xaml;

namespace Spectrum.Demo.Views
{
    public sealed partial class EditSchemeView
    {
        public EditSchemeView()
        {
            InitializeComponent();
        }

        private void OnGoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
