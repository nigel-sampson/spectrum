﻿using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Spectrum.Demo.Services;
using Spectrum.Demo.ViewModels.Editors;

namespace Spectrum.Demo.ViewModels
{
    public class EditSchemeViewModel : Screen
    {
        private readonly IWindowManager windowManager;
        private readonly ISchemeStorageService schemeStorage;
        private readonly INavigationService navigationService;

        public EditSchemeViewModel(IWindowManager windowManager, ISchemeStorageService schemeStorage, INavigationService navigationService)
        {
            this.windowManager = windowManager;
            this.schemeStorage = schemeStorage;
            this.navigationService = navigationService;

            Types = new BindableCollection<SchemeTypeViewModel>
            {
                new SchemeTypeViewModel(SchemeType.Complimentary, new ComplimentarySchemeEditorViewModel(windowManager)),
                new SchemeTypeViewModel(SchemeType.Analogous, new AnalogousSchemeEditorViewModel(windowManager)),
                new SchemeTypeViewModel(SchemeType.Triad, new TriadSchemeEditorViewModel(windowManager)),
                new SchemeTypeViewModel(SchemeType.Monochromatic, new MonochromeSchemeEditorViewModel(windowManager)),
                new SchemeTypeViewModel(SchemeType.Custom, new CustomSchemeEditorViewModel(windowManager))
            };

            SelectedType = Types[0];
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            if (Id == null)
                return;

            var schemes = await schemeStorage.GetSchemesAsync();
            
            Scheme = schemes.Single(s => s.Id == Id);
        }

        public Guid? Id
        {
            get; set;
        }

        public Scheme Scheme
        {
            get; set;
        }

        public BindableCollection<SchemeTypeViewModel> Types
        {
            get; private set;
        }

        public SchemeTypeViewModel SelectedType
        {
            get; set;
        }

        public async void Save()
        {
            if (Id == null)
            {
                var saveSchemeViewModel = new SaveSchemeViewModel();

                var confirmed = await windowManager.ShowDialogAsync(saveSchemeViewModel, settings: new Dictionary<string, object>
                {
                    { "Title", "Save Scheme"},
                    { "IsPrimaryButtonEnabled", true },
                    { "PrimaryButtonText", "save" },
                    { "IsSecondaryButtonEnabled", true },
                    { "SecondaryButtonText", "cancel" },
                    { "FullSizeDesired", false }
                });

                if (!confirmed)
                    return;

                var scheme = new Scheme
                {
                    Name = saveSchemeViewModel.Name,
                    CreatedOn = DateTimeOffset.Now,
                    Colours = SelectedType.Editor.GetColours().ToList(),
                    Type = SelectedType.Type
                };

                await schemeStorage.SaveSchemeAsync(scheme);
            }
            else
            {
                
            }

            navigationService.GoBack();
        }
    }
}