using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Text;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Sites.Items
{
    [QueryProperty("Item", "Item")]
    public partial class EditItemViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Item item;

        [ObservableProperty]
        private string price;

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
                double validPrice = double.Parse(Price);
                if (validPrice < 0)
                {
                    await Shell.Current.DisplayAlert("Ошибка!", "Цена должна быть положительным числом", "Ок");
                    return;
                }

                IsBusy = true;
                var response = await service.UpsertAsync(Item);

                if (response.Type == ResponseTypes.Ok)
                {
                    await Shell.Current.DisplayAlert("Внимание!", response.Message, "Ок");
                    await Shell.Current.Navigation.PopAsync(true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Ок");
                }

            }
            catch (FormatException)
            {
                await Shell.Current.DisplayAlert("Ошибка!", "Некорректная цена", "Ок");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Ок");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task DeleteItemAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var response = await service.DeleteAsync(Item.Id);

                if (response.Type == ResponseTypes.Ok)
                {
                    await Shell.Current.DisplayAlert("Внимание!", response.Message, "Oк");
                    await Shell.Current.Navigation.PopAsync(true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Oк");
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Oк");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
