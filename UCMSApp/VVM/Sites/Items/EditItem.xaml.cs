namespace UCMSApp.VVM.Sites.Items;

public partial class EditItem : ContentPage
{
	public EditItem(EditItemViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}