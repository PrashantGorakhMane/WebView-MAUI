using System;
using MAUISampleApp.Controls;
namespace MAUISampleApp.Interfaces
{
    public interface IHybridWebView : IView
    {
        event EventHandler<SourceChangedEventArgs> SourceChanged;
        event EventHandler<JavaScriptActionEventArgs> JavaScriptAction;
        event EventHandler<EvaluateJavaScriptAsyncRequest> RequestEvaluateJavaScript;

        void Refresh();

        Task EvaluateJavaScriptAsync(EvaluateJavaScriptAsyncRequest request);

        WebViewSource Source { get; set; }

        void Cleanup();

        void InvokeAction(string data);
        void RegisterAction(Action<string> action);

    }
}

