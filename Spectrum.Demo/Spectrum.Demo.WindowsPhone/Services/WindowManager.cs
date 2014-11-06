using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Caliburn.Micro;

namespace Spectrum.Demo.Services
{
    public class WindowManager : IWindowManager
    {
        public virtual async Task<bool> ShowDialogAsync(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            var view = LocateAndBindView(rootModel, context);

            var contentDialog = CreateDialog(view);

            //var page = GetCurrentPage();

            //ManageHostPage(page, contentDialog, opening: true);

            ApplySettings(contentDialog, settings);

            ActivateViewModel(rootModel);

            var result = await contentDialog.ShowAsync();

            DeactivateViewModel(rootModel);

            //ManageHostPage(page, contentDialog, opening: false);

            return result == ContentDialogResult.Primary;
        }

        //private static void ManageHostPage(Page page, Popup popup, bool opening)
        //{
        //    if (page == null)
        //        return;

        //    if (page.TopAppBar != null)
        //        page.TopAppBar.IsEnabled = !opening;

        //    if (page.BottomAppBar != null)
        //        page.BottomAppBar.IsEnabled = !opening;
        //}

        private static void DeactivateViewModel(object rootModel)
        {
            var deactivateViewModel = rootModel as IDeactivate;

            if (deactivateViewModel != null)
                deactivateViewModel.Deactivate(true);
        }

        private static void ActivateViewModel(object rootModel)
        {
            var activateViewModel = rootModel as IActivate;

            if (activateViewModel != null)
                activateViewModel.Activate();
        }

        private static ContentDialog CreateDialog(UIElement view)
        {
            var dialog = new ContentDialog
            {
                Content = view,
            };

            return dialog;
        }

        private static UIElement LocateAndBindView(object rootModel, object context)
        {
            var view = ViewLocator.LocateForModel(rootModel, null, context);

            ViewModelBinder.Bind(rootModel, view, context);

            return view;
        }

        protected virtual bool ApplySettings(object target, IEnumerable<KeyValuePair<string, object>> settings)
        {
            if (settings != null)
            {
                var type = target.GetType();

                foreach (var pair in settings)
                {
                    var propertyInfo = type.GetRuntimeProperty(pair.Key);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(target, pair.Value, null);
                    }
                }

                return true;
            }

            return false;
        }

        protected virtual Page GetCurrentPage()
        {
            var frame = Window.Current.Content as Frame;

            if (frame == null)
                return null;

            return frame.Content as Page;
        }
    }
}
