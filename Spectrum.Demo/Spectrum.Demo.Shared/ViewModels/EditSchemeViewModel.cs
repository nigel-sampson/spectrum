using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Caliburn.Micro;
using PropertyChanged;
using Spectrum.Demo.Services;
using Spectrum.Demo.ViewModels.Editors;

namespace Spectrum.Demo.ViewModels
{
    public class EditSchemeViewModel : Screen, ISupportSharing
    {
        private readonly IWindowManager windowManager;
        private readonly ISchemeStorageService schemeStorage;
        private readonly INavigationService navigationService;
        private readonly ISharingService sharingService;
        private readonly IBitmapService bitmapService;

        public EditSchemeViewModel(
            IWindowManager windowManager, 
            ISchemeStorageService schemeStorage, 
            INavigationService navigationService,
            ISharingService sharingService,
            IBitmapService bitmapService)
        {
            this.windowManager = windowManager;
            this.schemeStorage = schemeStorage;
            this.navigationService = navigationService;
            this.sharingService = sharingService;
            this.bitmapService = bitmapService;

            Types = new BindableCollection<SchemeTypeViewModel>
            {
                new SchemeTypeViewModel(SchemeType.Complimentary, new ComplimentarySchemeEditorViewModel(windowManager)),
                new SchemeTypeViewModel(SchemeType.Analogous, new AnalogousSchemeEditorViewModel(windowManager)),
                new SchemeTypeViewModel(SchemeType.Triad, new TriadSchemeEditorViewModel(windowManager)),
                new SchemeTypeViewModel(SchemeType.Monochromatic, new MonochromeSchemeEditorViewModel(windowManager)),
                new SchemeTypeViewModel(SchemeType.Custom, new CustomSchemeEditorViewModel(windowManager))
            };

            SelectedType = Types[0];

            foreach (var type in Types)
            {
                type.Editor.PropertyChanged += (s, e) =>
                {
                    NotifyOfPropertyChange(() => CanSave);
                    NotifyOfPropertyChange(() => CanShare);
                };
            }
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            if (Id == Guid.Empty)
                return;

            var schemes = await schemeStorage.GetSchemesAsync();
            
            Scheme = schemes.Single(s => s.Id == Id);

            SelectedType = Types.Single(t => t.Type == Scheme.Type);
            
            SelectedType.Editor.SetColors(Scheme.Colours);
        }

        public Guid Id
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
            if (Id == Guid.Empty)
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

                var scheme = CreateScheme(saveSchemeViewModel.Name);

                await schemeStorage.SaveSchemeAsync(scheme);
            }
            else
            {
                Scheme.Colours = SelectedType.Editor.GetColours().ToList();
                Scheme.Type = SelectedType.Type;

                await schemeStorage.SaveSchemeAsync(Scheme);
            }

            navigationService.GoBack();
        }

        private Scheme CreateScheme(string name)
        {
            var scheme = new Scheme
            {
                Name = name,
                CreatedOn = DateTimeOffset.Now,
                Colours = SelectedType.Editor.GetColours().ToList(),
                Type = SelectedType.Type
            };
            return scheme;
        }

        [DependsOn("SelectedType")]
        public bool CanSave
        {
            get
            {
                return SelectedType.Editor.CanSave;
            }
        }

        public void Share()
        {
            sharingService.ShowShareUI();
        }

        [DependsOn("SelectedType")]
        public bool CanShare
        {
            get { return SelectedType.Editor.CanSave; }
        }

        public async void Delete()
        {
            await schemeStorage.DeleteSchemeAsync(Scheme);

            navigationService.GoBack();
        }

        public bool CanDelete
        {
            get { return Id != Guid.Empty; }
        }

        public async void OnShareRequested(DataRequest dataRequest)
        {
            var deferral = dataRequest.GetDeferral();

            var scheme = CreateScheme(Scheme == null ? "Unamed scheme" : Scheme.Name);

            var imageUri = await bitmapService.CreateSchemeBitmapAsync(scheme, TileSize.Wide);

            var imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(imageUri));

            dataRequest.Data.Properties.Title = scheme.Name;
            dataRequest.Data.Properties.Description = "created by Spectrum";

            dataRequest.Data.SetStorageItems(new [] { imageFile });
            dataRequest.Data.SetText(String.Join(", ", scheme.Colours.Select(c => c.ToHexString())));

            deferral.Complete();
        }
    }
}
