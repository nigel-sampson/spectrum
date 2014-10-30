using System;
using Caliburn.Micro;
using Spectrum.Demo.Services;
using Spectrum.Demo.ViewModels.Editors;

namespace Spectrum.Demo.ViewModels
{
    public class SchemeTypeViewModel : PropertyChangedBase
    {
        private readonly SchemeType _type;
        private readonly SchemeEditorViewModelBase _editor;

        public SchemeTypeViewModel(SchemeType type, SchemeEditorViewModelBase editor)
        {
            _type = type;
            _editor = editor;
        }

        public SchemeType Type
        {
            get { return _type; }
        }

        public SchemeEditorViewModelBase Editor
        {
            get { return _editor; }
        }
    }
}
