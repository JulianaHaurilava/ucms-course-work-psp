using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UCMSApp.Services;
using UCMSApp.VVM.Base;
using UCMSApp.VVM.Sites.Items;

namespace UCMSApp.VVM.Sites
{
    [QueryProperty("Site", "Site")]
    public partial class SiteGenerationViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Site site;
        public ObservableCollection<Item> ValidItems { get; private set; } = new();

        private SiteService siteService;
        private ItemService itemService;

        private List<Item> items;
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

                loadedItems = loadedItems.Where(t => t.Site.Id == site.Id).ToList();
                items = loadedItems;

                ValidItems.Clear();
                foreach (Item item in items)
                {
                    ValidItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
            }
        }

        [RelayCommand]
        private async Task GoToEditItemWindowAsync(Item item)
        {
            try
            {
                IsBusy = true;
                await GoToChosenItem(item);
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо"); }
            finally { IsBusy = false; }
        }

        public async Task GoToChosenItem(Item item)
        {
            await Shell.Current.GoToAsync($"{nameof(EditItem)}", true, new Dictionary<string, object>
            {
                {"Item", item}
            });
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
