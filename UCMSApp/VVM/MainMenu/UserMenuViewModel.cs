using CommunityToolkit.Mvvm.Input;
using UCMSApp.VVM.Account;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.MainMenu
{
    public partial class UserMenuViewModel : BaseViewModel
    {
        public UserMenuViewModel()
        {
            Title = "Меню пользователя";
        }

        [RelayCommand]
        private async Task GoToAccountViewAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                await Shell.Current.GoToAsync($"{nameof(AccountView)}", true);

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

        [RelayCommand]
        private async Task GoToSitesViewAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                await Shell.Current.GoToAsync($"{nameof(Sites)}", true);

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
