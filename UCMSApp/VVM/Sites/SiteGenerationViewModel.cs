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
        private CategoryService categoryService;
        private ItemService itemService;

        public ObservableCollection<Category> Categories;
        public Category SelectedCategory;

        public ObservableCollection<Item> Items;
        public Category SelectedItem;

        /// <summary>
        /// АААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААа
        /// </summary>
        /// <param name="siteService"></param>
        /// <param name="categoryService"></param>
        /// <param name="itemService"></param>
        public SiteGenerationViewModel(SiteService siteService, CategoryService categoryService, ItemService itemService)
        {
            Title = "Редактирование сайта";
            this.siteService = siteService;
            this.categoryService = categoryService;
            this.itemService = itemService;
        }

        [RelayCommand]
        public async Task LoadCategoriesAsync()
        {
            try
            {
                List<Category> loadedCategories = await categoryService.GetAllAsync();

                if (loadedCategories.Count == 0)
                    return;

                loadedCategories = loadedCategories.Where(c => c.Site.Id == Site.Id).ToList();
                Categories = new ObservableCollection<Category>(loadedCategories);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
            }
        }

        [RelayCommand]
        public async Task LoadItemsAsync()
        {
            try
            {
                List<Item> loadedItems = await itemService.GetAllAsync();

                if (loadedItems.Count == 0)
                    return;

                loadedItems = loadedItems.Where(c => c.Category.Id == SelectedCategory.Id).ToList();
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
