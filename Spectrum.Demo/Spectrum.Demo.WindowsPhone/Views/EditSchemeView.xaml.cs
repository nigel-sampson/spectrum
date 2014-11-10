using System;
using Windows.UI.Xaml;

namespace Spectrum.Demo.Views
{
    public sealed partial class EditSchemeView
    {
        private static bool helpShownOnLoad;
        private bool helpVisible;

        public EditSchemeView()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (helpShownOnLoad)
            {
                HideHelp(false);
            }
            else
            {
                ShowHelp(false);
                helpShownOnLoad = true;
            }
        }

        private void ShowHelp(bool useTransitions = true)
        {
            helpVisible = true;

            VisualStateManager.GoToState(this, HelpVisible.Name, useTransitions);
        }

        private void HideHelp(bool useTransitions = true)
        {
            helpVisible = false;

            VisualStateManager.GoToState(this, HelpCollapsed.Name, useTransitions);
        }

        private void OnHideHelp(object sender, RoutedEventArgs e)
        {
            HideHelp();
        }

        private void OnToggleHelp(object sender, RoutedEventArgs e)
        {
            if (helpVisible)
                HideHelp();
            else
                ShowHelp();
        }
    }
}
