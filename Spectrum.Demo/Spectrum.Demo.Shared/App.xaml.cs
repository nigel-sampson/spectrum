using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Spectrum.Demo.Services;
using Spectrum.Demo.ViewModels;
using Spectrum.Demo.Views;

namespace Spectrum.Demo
{
    public sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            ConfigureSpecialValues();

            container = new WinRTContainer();

            container.RegisterWinRTServices();

            container
                .Singleton<ISchemeStorageService, SchemeStorageService>();

            container
                .PerRequest<SchemeListViewModel>()
                .PerRequest<EditSchemeViewModel>();
        }

        private static void ConfigureSpecialValues()
        {
            MessageBinder.SpecialValues.Add("$clickeditem", c => ((ItemClickEventArgs)c.EventArgs).ClickedItem);
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<SchemeListView>();
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }
    }
}