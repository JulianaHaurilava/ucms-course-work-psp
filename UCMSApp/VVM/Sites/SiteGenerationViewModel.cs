using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Sites
{
    [QueryProperty("Site", "Site")]
    public partial class SiteGenerationViewModel : BaseViewModel
    {
        [ObservableProperty] private CMSLib.DTO.Site site;

        private SiteService siteService;
        private ItemService itemService;

        public ObservableCollection<Item> Items;

        public SiteGenerationViewModel(SiteService siteService, ItemService itemService)
        {
            Title = "Редактирование сайта";
            this.siteService = siteService;
            this.itemService = itemService;
        }


        [RelayCommand]
        public async Task LoadItemsAsync()
        {
            try
            {
                List<Item> loadedItems = await itemService.GetAllAsync();

                if (loadedItems.Count == 0)
                    return;

                loadedItems = loadedItems.Where(c => c.Site.Id == site.Id).ToList();
                Items = new ObservableCollection<Item>(loadedItems);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
            }
        }

        [RelayCommand]
        private async Task UpsertSiteAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var response = await siteService.UpsertAsync(Site);

                if (response.Type == ResponseTypes.Ok)
                {
                    await Shell.Current.DisplayAlert("Внимание!", response.Message, "Хорошо");
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

        [RelayCommand]
        private async Task GenerateSiteAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var response = await siteService.GenerateSite(Site);

                if (response.Type == ResponseTypes.Ok)
                {
                    await Shell.Current.DisplayAlert("Внимание!", response.Message, "Хорошо");
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
    }
}
