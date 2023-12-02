using Microsoft.Extensions.Logging;
using System.Net;
using UCMSApp.Services;
using UCMSApp.VVM.Auth;
using UCMSApp.VVM.MainMenu;

namespace UCMSApp
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


            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<VVM.Auth.Authorization>();
            builder.Services.AddSingleton<AuthorizationViewModel>();

            builder.Services.AddSingleton<AdminMenu>();
            builder.Services.AddSingleton<AdminMenuViewModel>();
            builder.Services.AddSingleton<UserMenu>();
            builder.Services.AddSingleton<UserMenuViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            Client.Client client = new("127.0.0.1", 5050);
            client.Connect();

            return builder.Build();
        }
    }
}
