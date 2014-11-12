using System;
using Windows.UI.Xaml;

namespace Spectrum.Demo.Views.Editors
{
    public sealed partial class DerivedColourView : IColourView
    {
        public DerivedColourView()
        {
            this.InitializeComponent();
        }

        public bool DisplayOverlay
        {
            get { return OverlayText.Visibility == Visibility.Visible; }
            set { OverlayText.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}
