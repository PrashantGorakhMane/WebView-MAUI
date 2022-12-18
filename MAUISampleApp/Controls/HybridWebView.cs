using System;
using System.Windows.Input;
using MAUISampleApp.Interfaces;
namespace MAUISampleApp.Controls
{
    public class HybridWebView : View, IHybridWebView
    {
        public event EventHandler<SourceChangedEventArgs> SourceChanged;
        public event EventHandler<JavaScriptActionEventArgs> JavaScriptAction;
        public event EventHandler<EvaluateJavaScriptAsyncRequest> RequestEvaluateJavaScript;
        Action<string> action;
        //
        public static readonly BindableProperty CommandProperty =
           BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(HybridWebView), null);
        public static readonly BindableProperty CommandParameterProperty =
           BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(HybridWebView), null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public HybridWebView()
        {
            
        }

        public async Task EvaluateJavaScriptAsync(EvaluateJavaScriptAsyncRequest request)
        {
            await Task.Run(() =>
            {
                RequestEvaluateJavaScript?.Invoke(this, request);
            });
        }

        public void Refresh()
        {
            if (Source == null) return;
            var s = Source;
            Source = null;
            Source = s;
        }

        public WebViewSource Source
        {
            get { return (WebViewSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
          propertyName: "Source",
          returnType: typeof(WebViewSource),
          declaringType: typeof(HybridWebView),
          defaultValue: new UrlWebViewSource() { Url = "about:blank" },
          propertyChanged: OnSourceChanged);

        private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as HybridWebView;

            bindable.Dispatcher.Dispatch(() =>
            {
                view.SourceChanged?.Invoke(view, new SourceChangedEventArgs(newValue as WebViewSource));

            });
        }

        public void Cleanup()
        {
            JavaScriptAction = null;
        }

        public void InvokeAction(string data)
        {
            JavaScriptAction?.Invoke(this, new JavaScriptActionEventArgs(data));
            if (Command.CanExecute(CommandParameter))
            {
                object obj = new[] { CommandParameter, data };
                Command.Execute(obj);
            }
        }
        public void RegisterAction(Action<string> callback)
        {
            action = callback;
        }
    }
}

