using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Sites.Items
{
    [QueryProperty("Item", "Item")]
    public partial class EditItemViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Item item;

        private ItemService service;

        public EditItemViewModel(ItemService service)
        {
            this.service = service;
            Title = "Редактирование товара";
        }


        [RelayCommand]
        private async Task UpsertItemAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var response = await service.UpsertAsync(Item);

                if (response.Type == ResponseTypes.Ok)
                {
                    await Shell.Current.DisplayAlert("Внимание!", response.Message, "Хорошо");
                    await Shell.Current.Navigation.PopAsync(true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Хорошо");
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
