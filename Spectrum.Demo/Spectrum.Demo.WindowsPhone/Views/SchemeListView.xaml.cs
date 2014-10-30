using System;
using System.Threading;
using Windows.UI.Xaml.Navigation;
using Spectrum.Demo.Extensions;
using Spectrum.Universal;

namespace Spectrum.Demo.Views
{
    public sealed partial class SchemeListView
    {
        private CancellationTokenSource cancellationTokenSource;

        public SchemeListView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();

            AnimateTitleAsync(cancellationTokenSource.Token);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }

        private async void AnimateTitleAsync(CancellationToken cancellationToken)
        {
            var random = new Random();

            while (!cancellationToken.IsCancellationRequested)
            {
                var hue = random.Next(0, 360);

                var hsl = new Color.HSL(hue, 0.5, 0.5);

                AnimateTitleColorAnimation.To = hsl.ToRGB().ToSystemColor(255);

                await AnimateTitleStoryboard.BeginAsync();
            }
        }
    }
}
