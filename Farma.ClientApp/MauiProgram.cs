using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;
using farmaClientApp.Views;
using farmaClientApp.ViewModels;

namespace farmaClientApp
{
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
                });


            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
#if ANDROID
                h.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
                h.PlatformView.Background = null; // opcional, quita drawable por completo
#endif
#if IOS
    h.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });

            // Registrar páginas y ViewModels
            builder.Services.AddTransient<ClienteLoginPage>();
            builder.Services.AddTransient<ClienteLoginViewModel>();
            
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();
            
            builder.Services.AddTransient<NuevoPedidoPage>();
            builder.Services.AddTransient<NuevoPedidoViewModel>();
            
            builder.Services.AddTransient<MisPedidosPage>();
            builder.Services.AddTransient<MisPedidosViewModel>();
            
            builder.Services.AddTransient<ConfigurarPerfilPage>();
            builder.Services.AddTransient<ConfigurarPerfilViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
