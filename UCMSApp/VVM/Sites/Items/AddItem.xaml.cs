namespace UCMSApp.VVM.Sites.Items;

public partial class AddItem : ContentPage
{
	public AddItem(EditItemViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}