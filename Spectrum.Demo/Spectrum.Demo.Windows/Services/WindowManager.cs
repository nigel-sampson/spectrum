using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Caliburn.Micro;
using Spectrum.Demo.Controls;

namespace Spectrum.Demo.Services
{
    public class WindowManager : IWindowManager
    {
        public virtual Task<bool> ShowDialogAsync(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            var taskSource = new TaskCompletionSource<bool>();

            var view = LocateAndBindView(rootModel, context);

            var popup = CreatePopup(view, settings);
            var dialog = (DialogContentControl) popup.Child;

            var page = GetCurrentPage();

            ManageHostPage(page, popup, opening: true);

            EventHandler<DialogDismissedEventArgs> closed = null;

            closed = (s, o) =>
            {
                DeactivateViewModel(rootModel);

                ManageHostPage(page, popup, opening: false);

                taskSource.SetResult(o.Action == DialogAction.Primary);

                dialog.Dismissed -= closed;
            };

            dialog.Dismissed += closed;

            ActivateViewModel(rootModel);

            return taskSource.Task;
        }

        private static void ManageHostPage(Page page, Popup popup, bool opening)
        {
            if (page == null)
                return;

            var panel = page.Content as Panel;

            if (panel != null)
            {
                if (opening)
                    panel.Children.Add(popup);
                else
                {
                    panel.Children.Remove(popup);
                }
            }

        }

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

        private Popup CreatePopup(UIElement view, IEnumerable<KeyValuePair<string, object>> settings)
        {
            var dialogContent = new DialogContentControl
            {
                Content = view,
                Width = Window.Current.Bounds.Width,
                Height = Window.Current.Bounds.Height,
            };

            ApplySettings(dialogContent, settings);

            var popup = new Popup
            {
                Child = dialogContent,
                IsLightDismissEnabled = false,
                IsOpen = true,
                HorizontalOffset = 0.0d,
                VerticalOffset = 0.0d
            };

            dialogContent.Dismissed += (s, e) => popup.IsOpen = false;

            return popup;
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
