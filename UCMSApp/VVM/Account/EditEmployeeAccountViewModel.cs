using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Account
{
    [QueryProperty("User", "User")]
    public partial class EditEmployeeAccountViewModel : BaseViewModel
    {
        [ObservableProperty]
        private User user;

        private UserService service;

        public EditEmployeeAccountViewModel(UserService service)
        {
            this.service = service;
            Title = "Редактирование пользователя";
        }


        [RelayCommand]
        private async Task ChangeUserRoleAsync()
        {
            if (IsBusy) return;

            try
            {
                User.IsAdmin = !User.IsAdmin;
                IsBusy = true;
                var response = await service.UpsertAsync(User);

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

        [RelayCommand]
        private async Task DeleteChosenEmployeeAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var response = await service.DeleteAsync(User.Id);

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
