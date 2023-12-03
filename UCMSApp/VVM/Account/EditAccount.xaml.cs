namespace UCMSApp.VVM.Account;

public partial class EditAccount : ContentPage
{
	public EditAccount(EditAccountViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}