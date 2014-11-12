using System;

namespace Spectrum.Demo.Controls
{
    public class DialogDismissedEventArgs : EventArgs
    {
        private readonly DialogAction action;

        public DialogDismissedEventArgs(DialogAction action)
        {
            this.action = action;
        }

        public DialogAction Action
        {
            get { return action; }
        }
    }
}
