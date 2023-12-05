using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Net;
using UCMSApp.Services;
using UCMSApp.VVM.Account;
using UCMSApp.VVM.Auth;
using UCMSApp.VVM.MainMenu;
using UCMSApp.VVM.Registration;
using UCMSApp.VVM.Sites;
using UCMSApp.VVM.Sites.Items;
using UCMSApp.VVM.Templates;

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
                }).UseMauiCommunityToolkit();


            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<VVM.Auth.Authorization>();
            builder.Services.AddSingleton<AuthorizationViewModel>();

            builder.Services.AddTransient<Registration>();
            builder.Services.AddTransient<RegistrationViewModel>();

            builder.Services.AddSingleton<AdminMenu>();
            builder.Services.AddSingleton<AdminMenuViewModel>();
            builder.Services.AddSingleton<UserMenu>();
            builder.Services.AddSingleton<UserMenuViewModel>();

            builder.Services.AddTransient<AccountView>();
            builder.Services.AddTransient<AccountViewModel>();
            builder.Services.AddTransient<EditAccount>();
            builder.Services.AddTransient<EditAccountViewModel>();

            builder.Services.AddTransient<CompanyAccounts>();
            builder.Services.AddTransient<CompanyAccountsViewModel>();
            builder.Services.AddTransient<EditEmployeeAccount>();
            builder.Services.AddTransient<EditEmployeeAccountViewModel>();

            builder.Services.AddSingleton<SiteService>();
            builder.Services.AddTransient<Sites>();
            builder.Services.AddTransient<SitesViewModel>();
            builder.Services.AddTransient<SiteGeneration>();
            builder.Services.AddTransient<SiteGenerationViewModel>();
            builder.Services.AddTransient<AddSite>();
            builder.Services.AddTransient<AddSiteViewModel>();

            builder.Services.AddSingleton<ItemService>();
            builder.Services.AddTransient<EditItem>();
            builder.Services.AddTransient<AddItem>();
            builder.Services.AddTransient<EditItemViewModel>();

            builder.Services.AddSingleton<CompanyService>();

            builder.Services.AddSingleton<TemplateService>();
            builder.Services.AddTransient<Templates>();
            builder.Services.AddTransient<TemplatesViewModel>();
            builder.Services.AddTransient<EditTemplate>();
            builder.Services.AddTransient<AddTemplate>();
            builder.Services.AddTransient<EditTemplateViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            Client.Client client = new("127.0.0.1", 5050);
            client.Connect();

            return builder.Build();
        }
    }
}
