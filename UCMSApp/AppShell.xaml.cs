using System.Net;
using UCMSApp.VVM.Account;
using UCMSApp.VVM.MainMenu;
using UCMSApp.VVM.Registration;
using UCMSApp.VVM.Sites;
using UCMSApp.VVM.Sites.Items;

namespace UCMSApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AdminMenu), typeof(AdminMenu));
            Routing.RegisterRoute(nameof(UserMenu), typeof(UserMenu));
            Routing.RegisterRoute(nameof(Registration), typeof(Registration));

            Routing.RegisterRoute(nameof(AccountView), typeof(AccountView));
            Routing.RegisterRoute(nameof(EditAccount), typeof(EditAccount));

            Routing.RegisterRoute(nameof(Sites), typeof(Sites));
            Routing.RegisterRoute(nameof(SiteGeneration), typeof(SiteGeneration));
            Routing.RegisterRoute(nameof(AddSite), typeof(AddSite));
            Routing.RegisterRoute(nameof(EditItem), typeof(EditItem));
            Routing.RegisterRoute(nameof(AddItem), typeof(AddItem));

            Routing.RegisterRoute(nameof(CompanyAccounts), typeof(CompanyAccounts));
            Routing.RegisterRoute(nameof(EditEmployeeAccount), typeof(EditEmployeeAccount));
        }
    }
}
