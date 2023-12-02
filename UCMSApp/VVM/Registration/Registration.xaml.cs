namespace UCMSApp.VVM.Registration;

public partial class Registration : ContentPage
{
	public Registration(RegistrationViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}