using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Spectrum.Demo.ViewModels.Editors;

namespace Spectrum.Demo.Views.Editors
{
    public sealed partial class ComplimentarySchemeEditorView
    {
        public ComplimentarySchemeEditorView()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private SingleColorSchemeEditorViewModelBase ViewModel
        {
            get { return (SingleColorSchemeEditorViewModelBase)DataContext; }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Observable.FromEventPattern<PointerRoutedEventArgs>(InputPanel, "PointerMoved")
                .Where(ev => !ev.EventArgs.Handled)
                .Select(ev => ev.EventArgs.GetCurrentPoint(InputPanel))
                .Select(p => new
                {
                    Width = p.Position.X / InputPanel.ActualWidth,
                    Height = p.Position.Y / InputPanel.ActualHeight
                })
                .Subscribe(p =>
                {
                    ViewModel.CurrentHue = p.Width * 360.0d;
                    ViewModel.CurrentLuminosity = p.Height;

                    ViewModel.UpdateColor();
                });

            Observable.FromEventPattern<RangeBaseValueChangedEventArgs>(SaturationSlider, "ValueChanged")
                .Select(ev => ev.EventArgs.NewValue)
                .Subscribe(s =>
                {
                    ViewModel.CurrentSaturation = s;

                    ViewModel.UpdateColor();
                });
        }
    }
}
