namespace UCMSApp.VVM.Templates;

public partial class Templates : ContentPage
{
	public Templates(TemplatesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}