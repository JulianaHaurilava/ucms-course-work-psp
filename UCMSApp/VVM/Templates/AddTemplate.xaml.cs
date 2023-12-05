namespace UCMSApp.VVM.Templates;

public partial class AddTemplate : ContentPage
{
	public AddTemplate(EditTemplateViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}