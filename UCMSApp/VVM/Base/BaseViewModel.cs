using CommunityToolkit.Mvvm.ComponentModel;

namespace UCMSApp.VVM.Base
{
	public partial class BaseViewModel : ObservableObject
	{
		[ObservableProperty] private bool isBusy;
		[ObservableProperty] string title;
	}
}
