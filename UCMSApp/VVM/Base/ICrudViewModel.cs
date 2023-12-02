namespace UCMSApp.VVM.Base
{
	internal interface ICrudViewModel<T>
	{
		public Task LoadElementsAsync();
		public Task GoToAddElementPageAsync(T element);
		public Task UpsertElementAsync(T element);
		public Task DeleteElementAsync(int id);
	}
}
