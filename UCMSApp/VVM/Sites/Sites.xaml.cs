namespace UCMSApp.VVM.Sites;

public partial class Sites : ContentPage
{
	public Sites(SitesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}