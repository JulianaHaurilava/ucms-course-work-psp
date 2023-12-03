namespace UCMSApp.VVM.MainMenu;

public partial class AdminMenu : ContentPage
{
	public AdminMenu(AdminMenuViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}