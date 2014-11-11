using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Spectrum.Demo.Views.Editors
{
    public sealed partial class ColourView : IColourView
    {
        public ColourView()
        {
            InitializeComponent();
        }

        public bool CanEdit
        {
            get { return Edit.Visibility == Visibility.Visible; }
            set { Edit.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool CanRemove
        {
            get { return Remove.Visibility == Visibility.Visible; }
            set { Remove.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        private void OnHandlePointerMoved(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        public bool DisplayOverlay
        {
            get { return OverlayPanel.Visibility == Visibility.Visible; }
            set { OverlayPanel.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}
