using CMSLib.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Account
{
    public partial class AccountViewModel : BaseViewModel
    {
        [ObservableProperty] private User account;

        public AccountViewModel()
        {
            account = Client.Client.Instance.CurrentUser;
            Title = "Аккаунт";
        }

        public async Task GoToAddElementPageAsync(User user)
        {
            //await Shell.Current.GoToAsync($"{nameof(EditUserForUser)}", true, new Dictionary<string, object>
            //{
            //    {"User", user}
            //});
        }

        [RelayCommand]
        public async Task UpsertElementAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                await GoToAddElementPageAsync(Account);
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
