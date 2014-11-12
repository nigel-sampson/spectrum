using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Spectrum.Demo.Controls
{
    [TemplatePart(Type = typeof(FrameworkElement), Name = BackgroundElementName)]
    [TemplatePart(Type = typeof(ButtonBase), Name = PrimaryButtonName)]
    [TemplatePart(Type = typeof(ButtonBase), Name = SecondaryButtonName)]
    public class DialogContentControl : ContentControl
    {
        private const string BackgroundElementName = "BackgroundElement";
        private const string PrimaryButtonName = "PrimaryButton";
        private const string SecondaryButtonName = "SecondaryButton";

        public static readonly DependencyProperty TitleProperty = 
            DependencyProperty.Register("Title", typeof (string), typeof (DialogContentControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty PrimaryButtonTextProperty = 
            DependencyProperty.Register("PrimaryButtonText", typeof (string), typeof (DialogContentControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty SecondaryButtonTextProperty = 
            DependencyProperty.Register("SecondaryButtonText", typeof (string), typeof (DialogContentControl), new PropertyMetadata(default(string)));

        public DialogContentControl()
        {
            DefaultStyleKey = typeof(DialogContentControl);
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string SecondaryButtonText
        {
            get { return (string)GetValue(SecondaryButtonTextProperty); }
            set { SetValue(SecondaryButtonTextProperty, value); }
        }

        public string PrimaryButtonText
        {
            get { return (string)GetValue(PrimaryButtonTextProperty); }
            set { SetValue(PrimaryButtonTextProperty, value); }
        }

        public event EventHandler<DialogDismissedEventArgs> Dismissed;

        protected virtual void OnDismissed(DialogDismissedEventArgs e)
        {
            var dismissed = Dismissed;

            if (dismissed != null)
                dismissed(this, e);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var backgroundElement = GetTemplateChild(BackgroundElementName) as FrameworkElement;

            if (backgroundElement != null)
            {
                backgroundElement.Tapped += (s, e) => OnDismissed(new DialogDismissedEventArgs(DialogAction.Other));
            }

            var primaryButton = GetTemplateChild(PrimaryButtonName) as ButtonBase;

            if (primaryButton != null)
            {
                primaryButton.Click += (s, e) => OnDismissed(new DialogDismissedEventArgs(DialogAction.Primary));
            }

            var secondaryButton = GetTemplateChild(SecondaryButtonName) as ButtonBase;

            if (secondaryButton != null)
            {
                secondaryButton.Click += (s, e) => OnDismissed(new DialogDismissedEventArgs(DialogAction.Secondary));
            }
        }
    }
}
