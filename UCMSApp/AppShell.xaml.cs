using UCMSApp.VVM.MainMenu;
using UCMSApp.VVM.Registration;

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
        }
    }
}
