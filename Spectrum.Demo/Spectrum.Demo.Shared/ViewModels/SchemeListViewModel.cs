using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Spectrum.Demo.Services;

namespace Spectrum.Demo.ViewModels
{
    public class SchemeListViewModel : Screen
    {
        private readonly INavigationService navigation;
        private readonly ISchemeStorageService schemeStorage;

        public SchemeListViewModel(INavigationService navigation, ISchemeStorageService schemeStorage)
        {
            this.navigation = navigation;
            this.schemeStorage = schemeStorage;

            Schemes = new BindableCollection<SchemeViewModel>();
        }

        protected override async void OnInitialize()
        {
            var schemes = await schemeStorage.GetSchemesAsync();

            Schemes.AddRange(schemes.Select(s => new SchemeViewModel(s)));
        }

        public void Create()
        {
            navigation.UriFor<EditSchemeViewModel>()
                .Navigate();
        }

        public void View(SchemeViewModel schemeViewModel)
        {
            navigation.UriFor<EditSchemeViewModel>()
                .WithParam(v => v.Id, schemeViewModel.Scheme.Id)
               .Navigate();
        }

        public BindableCollection<SchemeViewModel> Schemes
        {
            get; private set;
        }
    }
}
