using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Spectrum.Demo.ViewModels.Editors;
using WinRTXamlToolkit.Tools;

namespace Spectrum.Demo.Views.Editors
{
    public abstract class SingleColorSchemeEditorViewBase : UserControl
    {
        protected SingleColorSchemeEditorViewBase()
        {
            Loaded += OnLoaded;
        }

        private SingleColorSchemeEditorViewModelBase ViewModel
        {
            get { return (SingleColorSchemeEditorViewModelBase) DataContext; }
        }

        protected abstract Panel GetInputPanel();
        
        protected abstract Slider GetSaturationSlider();

        protected abstract IEnumerable<IColourView> GetColourViews();

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var inputPanel = GetInputPanel();
            var saturatonSlider = GetSaturationSlider();
            var colourViews = GetColourViews().ToList();

            Observable.FromEventPattern<PointerRoutedEventArgs>(inputPanel, "PointerPressed")
                .Subscribe(ev => colourViews.ForEach(c => c.DisplayOverlay = false));

            Observable.FromEventPattern<PointerRoutedEventArgs>(inputPanel, "PointerReleased")
                 .Subscribe(ev => colourViews.ForEach(c => c.DisplayOverlay = true));

            Observable.FromEventPattern<PointerRoutedEventArgs>(inputPanel, "PointerMoved")
                .Where(ev => !ev.EventArgs.Handled && ev.EventArgs.Pointer.IsInContact)
                .Select(ev => ev.EventArgs.GetCurrentPoint(inputPanel))
                .Select(p => new
                {
                    Width = p.Position.X / inputPanel.ActualWidth,
                    Height = p.Position.Y / inputPanel.ActualHeight
                })
                .Subscribe(p =>
                {
                    ViewModel.CurrentHue = p.Width * 360.0d;
                    ViewModel.CurrentLuminosity = p.Height;

                    ViewModel.UpdateColor();
                });

            Observable.FromEventPattern<RangeBaseValueChangedEventArgs>(saturatonSlider, "ValueChanged")
                .Select(ev => ev.EventArgs.NewValue)
                .Subscribe(s =>
                {
                    ViewModel.CurrentSaturation = s;

                    ViewModel.UpdateColor();
                });
        }
    }
}
