using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Spectrum.Demo.Services;
using Spectrum.Universal;
using WinColor = Windows.UI.Color;

namespace Spectrum.Demo.ViewModels.Editors
{
    public abstract class SchemeEditorViewModelBase : Screen
    {
        private readonly IWindowManager windowManager;

        protected SchemeEditorViewModelBase(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        public abstract IEnumerable<Color.RGB> GetColours();

        public async void Edit(ColourViewModel color)
        {
            var editColourViewModel = new EditColourViewModel(color.Color.FromSystemColor());

            var confirmed = await windowManager.ShowDialogAsync(editColourViewModel, settings: new Dictionary<string, object>
            {
                { "Title", "Edit Color"},
                { "IsPrimaryButtonEnabled", true },
                { "PrimaryButtonText", "save" },
                { "IsSecondaryButtonEnabled", true },
                { "SecondaryButtonText", "cancel" },
                { "FullSizeDesired", false }
            });

            if (!confirmed)
                return;

            color.Color = editColourViewModel.Colour.ToSystemColor(255);
        }

        public abstract void SetColors(IEnumerable<Color.RGB> colours);

        public abstract bool CanSave { get; }
    }
}
