namespace UCMSApp.VVM.Account;

public partial class EditEmployeeAccount : ContentPage
{
	public EditEmployeeAccount(EditEmployeeAccountViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}