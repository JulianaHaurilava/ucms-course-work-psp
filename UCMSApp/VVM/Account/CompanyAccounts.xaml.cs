namespace UCMSApp.VVM.Account;

public partial class CompanyAccounts : ContentPage
{
	public CompanyAccounts(CompanyAccountsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}