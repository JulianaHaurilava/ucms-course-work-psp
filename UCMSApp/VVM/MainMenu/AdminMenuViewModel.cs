using CommunityToolkit.Mvvm.Input;
using UCMSApp.VVM.Account;
using UCMSApp.VVM.Base;
using UCMSApp.VVM.Sites;

namespace UCMSApp.VVM.MainMenu
{
    public partial class AdminMenuViewModel : BaseViewModel
    {
        public AdminMenuViewModel()
        {
            Title = "Панель руководителя";
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
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Ок");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToCompanyViewAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                await Shell.Current.GoToAsync($"{nameof(CompanyAccounts)}", true);

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
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Ок");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToSamplesViewAsync()
        {
            //if (IsBusy) return;
            //try
            //{
            //    IsBusy = true;
            //    await Shell.Current.GoToAsync($"{nameof(Cinemas)}", true);

            //}
            //catch (Exception ex)
            //{
            //    await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
            //}
            //finally
            //{
            //    IsBusy = false;
            //}
        }
    }
}
