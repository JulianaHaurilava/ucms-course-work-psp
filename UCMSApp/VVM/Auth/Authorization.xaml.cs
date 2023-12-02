namespace UCMSApp.VVM.Auth;

public partial class Authorization : ContentPage
{
	public Authorization(AuthorizationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}