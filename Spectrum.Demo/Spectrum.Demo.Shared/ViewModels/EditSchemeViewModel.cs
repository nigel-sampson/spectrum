using System;
using Caliburn.Micro;
using Spectrum.Demo.Services;
using Spectrum.Demo.ViewModels.Editors;

namespace Spectrum.Demo.ViewModels
{
    public class EditSchemeViewModel : Screen
    {
        public EditSchemeViewModel()
        {
            Types = new BindableCollection<SchemeTypeViewModel>
            {
                new SchemeTypeViewModel(SchemeType.Complimentary, null),
                new SchemeTypeViewModel(SchemeType.Analogous, null),
                new SchemeTypeViewModel(SchemeType.Triad, null),
                new SchemeTypeViewModel(SchemeType.Monochromatic, null),
                new SchemeTypeViewModel(SchemeType.Custom, new CustomSchemeEditorViewModel())
            };

            SelectedType = Types[0];
        }

        public Guid? Id
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
    }
}
