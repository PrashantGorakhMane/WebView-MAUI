#if ANDROID
using MAUISampleApp.Platforms.Android.Handlers;
#endif

#if IOS
using MAUISampleApp.Platforms.iOS.Handlers;
#endif
using MAUISampleApp.Interfaces;
using MAUISampleApp.Controls;
using MAUISampleApp.ViewModels;

namespace MAUISampleApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
        .ConfigureMauiHandlers(handlers =>
               {
                   handlers.AddHandler(typeof(Controls.HybridWebView), typeof(HybridWebViewHandler));
               });
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddSingleton<ICameraService, CameraFeatures>();
        return builder.Build();

    }
}

