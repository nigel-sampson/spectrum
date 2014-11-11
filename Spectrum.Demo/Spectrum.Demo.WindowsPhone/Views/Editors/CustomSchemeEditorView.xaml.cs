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
        private double currentHue;
        private double currentSaturation = 0.5d;
        private double currentLuminosity;

        public CustomSchemeEditorView()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private CustomSchemeEditorViewModel ViewModel
        {
            get { return (CustomSchemeEditorViewModel) DataContext; }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var random = new Random();

            currentHue = random.Next(0, 360);
            currentLuminosity = random.NextDouble();

            UpdateColor();

            Observable.FromEventPattern<PointerRoutedEventArgs>(InputPanel, "PointerPressed")
               .Subscribe(ev => Description.Visibility = Visibility.Collapsed);

            Observable.FromEventPattern<PointerRoutedEventArgs>(InputPanel, "PointerReleased")
                 .Subscribe(ev => Description.Visibility = Visibility.Visible);

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
                    currentHue = p.Width * 360.0d;
                    currentLuminosity = p.Height;

                    UpdateColor();
                });

            Observable.FromEventPattern<RangeBaseValueChangedEventArgs>(SaturationSlider, "ValueChanged")
                .Select(ev => ev.EventArgs.NewValue)
                .Subscribe(s =>
                {
                    currentSaturation = s;

                    UpdateColor();
                });

            Observable.FromEventPattern<PointerRoutedEventArgs>(InputPanel, "PointerReleased")
                .Where(ev => ViewModel.Colors.Count < 6)
                .Subscribe(ev =>
                {
                    var hsl = new Color.HSL(currentHue, currentSaturation, currentLuminosity);
                    var rgb = hsl.ToRGB();

                    ViewModel.Add(rgb.ToSystemColor(255));
                });
        }

        private void UpdateColor()
        {
            var hsl = new Color.HSL(currentHue, currentSaturation, currentLuminosity);
            var rgb = hsl.ToRGB();

            InputPanel.Background = new SolidColorBrush(rgb.ToSystemColor(255));

            Description.Text = rgb.ToHexString();
            Description.Foreground = new SolidColorBrush(hsl.L < 0.5d ? Windows.UI.Colors.White : Windows.UI.Colors.Black);
        }
    }
}
