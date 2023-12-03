namespace UCMSApp.VVM.Sites;

public partial class SiteGeneration : ContentPage
{
	public SiteGeneration(SiteGenerationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}