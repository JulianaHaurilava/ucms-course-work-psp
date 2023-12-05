namespace UCMSApp.VVM.Templates;

public partial class EditTemplate : ContentPage
{
	public EditTemplate(EditTemplateViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}