using CMSLib.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Sites
{
    public enum SiteOrderType
    {
        NameFromAToZ,
        NameFromZToA
    }

    public partial class SitesViewModel : BaseViewModel
    {
        public ObservableCollection<Site> ValidSites { get; private set; } = new();

        [ObservableProperty] private string searchString;
        [ObservableProperty] private List<string> orderTypesList = new() { "От А до Я", "От Я до А" };
        [ObservableProperty] private int orderTypeId = (int)SiteOrderType.NameFromAToZ;

        private List<Site> sites;
        private SiteService service;

        public SitesViewModel(SiteService service)
        {
            Title = "Сайты";
            this.service = service;
        }

        [RelayCommand]
        public async Task LoadElementsAsync()
        {
            try
            {
                List<Site> loadedSites = await service.GetAllAsync();

                if (loadedSites.Count == 0)
                    return;

                loadedSites = loadedSites.Where(t => t.CompanyId == Client.Client.Instance.CurrentUser.Company.Id).ToList();
                sites = loadedSites;
                ValidateElements();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
            }
        }

        private bool CanAdd()
        {
            return Client.Client.Instance.CurrentUser.IsAdmin;
        }

        [RelayCommand(CanExecute = nameof(CanAdd))]
        private async Task AddSiteAsync(Site site)
        {
            try
            {
                IsBusy = true;
                site ??= new Site { Company = Client.Client.Instance.CurrentUser.Company };
                await GoToAddSiteWindow(site);
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо"); }
            finally { IsBusy = false; }
        }

        private async Task GoToAddSiteWindow(Site site)
        {
            await Shell.Current.GoToAsync(nameof(AddSite), true, new Dictionary<string, object>()
            {
                {"Site", site}
            });
        }


        [RelayCommand]
        private async Task GenerateSiteAsync(Site site)
        {
            try
            {
                IsBusy = true;
                await GoToSiteGeneration(site);
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо"); }
            finally { IsBusy = false; }
        }

        private async Task GoToSiteGeneration(Site site)
        {
            await Shell.Current.GoToAsync(nameof(SiteGeneration), true, new Dictionary<string, object>()
            {
                {"Site", site}
            });
        }

        [RelayCommand]
        private void ValidateElements()
        {
            List<Site> searchedSites = SearchElements(sites);
            var searchedAndOrderedSites = OrderElements(searchedSites);

            ValidSites.Clear();
            foreach (Site site in searchedAndOrderedSites)
            {
                ValidSites.Add(site);
            }
        }

        private List<Site> SearchElements(List<Site> unsearchedSites)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return unsearchedSites;
            }

            List<Site> searchedSites = new();
            searchedSites = unsearchedSites.Where(f => f.Name.Contains(SearchString)).ToList();
            return searchedSites;
        }

        private List<Site> OrderElements(List<Site> unorderedSites)
        {
            List<Site> orderedSites = new();
            var orderType = (SiteOrderType)OrderTypeId;
            switch (orderType)
            {
                case SiteOrderType.NameFromAToZ:
                    orderedSites = unorderedSites.OrderBy((f) => f.Name).ToList();
                    break;
                case SiteOrderType.NameFromZToA:
                    orderedSites = unorderedSites.OrderByDescending((f) => f.Name).ToList();
                    break;
            }
            return orderedSites;
        }

    }
}
