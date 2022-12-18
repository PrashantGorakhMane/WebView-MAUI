using System;
namespace MAUISampleApp.Controls
{
    public class SourceChangedEventArgs : EventArgs
    {
        public WebViewSource Source
        {
            get;
            private set;
        }

        public SourceChangedEventArgs(WebViewSource source)
        {
            Source = source;
        }
    }
}

