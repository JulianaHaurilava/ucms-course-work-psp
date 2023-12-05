namespace UCMSApp.VVM.Sites;

public partial class AddSite : ContentPage
{
	public AddSite(AddSiteViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}