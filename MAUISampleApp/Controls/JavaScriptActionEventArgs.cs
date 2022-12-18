using System;
namespace MAUISampleApp.Controls
{
    public class JavaScriptActionEventArgs : EventArgs
    {
        public string Payload { get; private set; }

        public JavaScriptActionEventArgs(string payload)
        {
            Payload = payload;
        }
    }
}

