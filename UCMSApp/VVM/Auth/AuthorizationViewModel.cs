using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Auth
{
    public partial class AuthorizationViewModel : BaseViewModel
    {
        [ObservableProperty] private User user = new();

        private UserService userService;

        public AuthorizationViewModel(UserService userService)
        {
            Title = "Авторизация";
            this.userService = userService;
        }

        private async Task GoToRegistrationPage()
        {
            //await Shell.Current.GoToAsync($"{nameof(Registration)}", true);
        }

        private async Task GoToMainPageAsync(User user)
        {
            if (user == null)
                return;

            //if (user.IsAdmin)
            //{
            //    await Shell.Current.GoToAsync($"{nameof(AdminPage)}", true, new Dictionary<string, object>
            //    {
            //        {"user", User}
            //    });
            //}
            //else
            //{
            //    await Shell.Current.GoToAsync($"{nameof(UserPage)}", true, new Dictionary<string, object>
            //    {
            //        {"user", User}
            //    });
            //}

        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            await GoToRegistrationPage();
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var response = await userService.AuthorizeAsync(User);
                if (response != null)
                {
                    if (response.Type == ResponseTypes.Ok)
                    {
                        User responseUser = JsonConvert.DeserializeObject<User>(response.Data);
                        Client.Client.Instance.CurrentUser = responseUser;
                        await GoToMainPageAsync(responseUser);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Хорошо");
                    }
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
