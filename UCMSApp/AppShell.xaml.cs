using UCMSApp.VVM.MainMenu;

namespace UCMSApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AdminMenu), typeof(AdminMenu));
            Routing.RegisterRoute(nameof(UserMenu), typeof(UserMenu));
        }
    }
}
