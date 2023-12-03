namespace UCMSApp.VVM.MainMenu;

public partial class UserMenu : ContentPage
{
	public UserMenu(UserMenuViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}