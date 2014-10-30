using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Spectrum.Demo.ViewModels.Editors;
using Spectrum.Universal;

namespace Spectrum.Demo.Views.Editors
{
    public sealed partial class CustomSchemeEditorView
    {
        private double _currentHue;
        private double _currentSaturation = 0.5d;
        private double _currentLuminosity;

        public CustomSchemeEditorView()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private SchemeEditorViewModelBase ViewModel
        {
            get { return (SchemeEditorViewModelBase) DataContext; }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Observable.FromEventPattern<PointerRoutedEventArgs>(InputPanel, "PointerMoved")
                .Select(ev => ev.EventArgs.GetCurrentPoint(InputPanel))
                .Select(p => new
                {
                    Width = p.Position.X / InputPanel.ActualWidth,
                    Height = p.Position.Y / InputPanel.ActualHeight
                })
                .Subscribe(p =>
                {
                    _currentHue = p.Width * 360.0d;
                    _currentLuminosity = p.Height;

                    UpdateColor();
                });

            Observable.FromEventPattern<RangeBaseValueChangedEventArgs>(SaturationSlider, "ValueChanged")
                .Select(ev => ev.EventArgs.NewValue)
                .Subscribe(s =>
                {
                    _currentSaturation = s;

                    UpdateColor();
                });

            Observable.FromEventPattern<TappedRoutedEventArgs>(InputPanel, "Tapped")
                .Where(ev => ViewModel.Colors.Count < 6)
                .Subscribe(ev =>
                {
                    var hsl = new Color.HSL(_currentHue, _currentSaturation, _currentLuminosity);
                    var rgb = hsl.ToRGB();

                    ViewModel.Add(rgb.ToSystemColor(255));
                });
        }

        private void UpdateColor()
        {
            var hsl = new Color.HSL(_currentHue, _currentSaturation, _currentLuminosity);
            var rgb = hsl.ToRGB();

            InputPanel.Background = new SolidColorBrush(rgb.ToSystemColor(255));

            Description.Text = rgb.ToHexString();
            Description.Foreground = new SolidColorBrush(hsl.L < 0.5d ? Windows.UI.Colors.White : Windows.UI.Colors.Black);
        }
    }
}
