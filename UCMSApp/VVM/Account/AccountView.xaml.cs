namespace UCMSApp.VVM.Account;

public partial class AccountView : ContentPage
{
	public AccountView(AccountViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}