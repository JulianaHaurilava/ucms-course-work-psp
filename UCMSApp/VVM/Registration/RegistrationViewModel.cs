using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Registration
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        [ObservableProperty]
        private User user = new();
        [ObservableProperty]
        private string confirmPassword = "";
        [ObservableProperty]
        private string selectedCompany = "";

        private UserService userService;
        private CompanyService companyService;

        public RegistrationViewModel(UserService userService, CompanyService companyService)
        {
            Title = "Регистрация";
            this.userService = userService;
            this.companyService = companyService;
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

                Company company = await GetCompanyAsync();
                User.Company = company;
                if (company.Id == 0)
                {
                    User.IsAdmin = true;
                }

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

        private async Task<Company> GetCompanyAsync()
        {
            Company company = new Company { Name = SelectedCompany };
            try
            {
                IsBusy = true;
                List<Company> loadedCompanies = await companyService.GetAllAsync();

                if (loadedCompanies.Count == 0)
                    return company;

                foreach (Company c in loadedCompanies)
                {
                    if (c.Name == SelectedCompany)
                    {
                        company = c;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
            }
            finally { IsBusy = false; }

            return company;
        }

        //private async Task GoToAuthView(User user)
        //{
        //    if (user == null)
        //        return;

        //    await Shell.Current.Navigation.PopAsync(true);
        //}
    }
}
