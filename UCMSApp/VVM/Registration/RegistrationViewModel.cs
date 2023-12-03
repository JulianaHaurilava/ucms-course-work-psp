using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net;
using UCMSApp.Services;
using UCMSApp.VVM.Base;
using UCMSApp.VVM.MainMenu;

namespace UCMSApp.VVM.Registration
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        [ObservableProperty] private User user = new();
        [ObservableProperty] private string confirmPassword = "";

        private UserService userService;

        public RegistrationViewModel(UserService userService)
        {
            Title = "Регистрация";
            this.userService = userService;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                //if (!await CheckUserPropertiesAsync())
                //    return;

                var response = await userService.UpsertAsync(User);
                if (response.Type == ResponseTypes.Ok)
                {
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

        //private async Task GoToAuthView(User user)
        //{
        //    if (user == null)
        //        return;

        //    await Shell.Current.Navigation.PopAsync(true);
        //}
    }
}
