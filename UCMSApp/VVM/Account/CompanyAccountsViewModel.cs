using CMSLib.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Account
{
    public enum SiteOrderType
    {
        NameFromAToZ,
        NameFromZToA
    }
    public partial class CompanyAccountsViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; private set; } = new();

        [ObservableProperty] private string searchString;
        [ObservableProperty] private List<string> orderTypesList = new() { "От А до Я", "От Я до А" };
        [ObservableProperty] private int orderTypeId = (int)SiteOrderType.NameFromAToZ;

        private List<User> users;
        private UserService service;

        public CompanyAccountsViewModel(UserService service)
        {
            Title = "Пользователи компании";
            this.service = service;
        }

        [RelayCommand]
        public async Task LoadElementsAsync()
        {
            try
            {
                List<User> loadedUsers = await service.GetAllAsync();

                if (loadedUsers.Count == 0)
                    return;

                loadedUsers = loadedUsers.Where(t => t.Company.Id == Client.Client.Instance.CurrentUser.Company.Id ).ToList();
                users = loadedUsers;
                ValidateElements();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
            }
        }

        [RelayCommand]
        private async Task EditEmployeeAccountAsync(User user)
        {
            if (user.Id == Client.Client.Instance.CurrentUser.Id)
            {
                await Shell.Current.DisplayAlert("Ошибка!", "Редактирование собственного аккаунта невозможно в данном режиме", "Oк");
                return;
            }
            try
            {
                IsBusy = true;
                await GoToChosenUser(user);
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо"); }
            finally { IsBusy = false; }
        }

        private async Task GoToChosenUser(User user)
        {
            await Shell.Current.GoToAsync(nameof(EditEmployeeAccount), true, new Dictionary<string, object>()
            {
                {"User", user}
            });
        }

        [RelayCommand]
        private void ValidateElements()
        {
            List<User> searchedUsers = SearchElements(users);
            var searchedAndOrderedUsers = OrderElements(searchedUsers);

            Users.Clear();
            foreach (User user in searchedAndOrderedUsers)
            {
                Users.Add(user);
            }
        }

        private List<User> SearchElements(List<User> unsearched)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return unsearched;
            }

            List<User> searched = new();
            searched = unsearched.Where(f => f.Email.Contains(SearchString)).ToList();
            return searched;
        }

        private List<User> OrderElements(List<User> unordered)
        {
            List<User> ordered = new();
            var orderType = (SiteOrderType)OrderTypeId;
            switch (orderType)
            {
                case SiteOrderType.NameFromAToZ:
                    ordered = unordered.OrderBy((f) => f.Email).ToList();
                    break;
                case SiteOrderType.NameFromZToA:
                    ordered = unordered.OrderByDescending((f) => f.Email).ToList();
                    break;
            }
            return ordered;
        }
    }
}
