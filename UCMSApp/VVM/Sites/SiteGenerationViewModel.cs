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

        [ObservableProperty]
        private Template chosenTemplate;
        public ObservableCollection<Item> ValidItems { get; private set; } = new();

        private SiteService siteService;
        private ItemService itemService;
        private TemplateService templateService;

        private List<Item> items;
        public ObservableCollection<Item> Items;

        private List<Template> templates;
        public ObservableCollection<Template> Templates;

        public SiteGenerationViewModel(SiteService siteService, ItemService itemService, TemplateService templateService)
        {
            Title = "Редактирование сайта";
            this.siteService = siteService;
            this.itemService = itemService;
            this.templateService = templateService;
        }

        [RelayCommand]
        public async Task LoadAllDataAsync()
        {
            await LoadItemsAsync();
            await LoadTemplatesAsync();
        }

        [RelayCommand]
        public async Task LoadItemsAsync()
        {
            try
            {
                List<Item> loadedItems = await itemService.GetAllAsync();

                if (loadedItems.Count == 0)
                    return;

                items = loadedItems.Where(t => t.Site.Id == Site.Id).ToList();

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
        public async Task LoadTemplatesAsync()
        {
            try
            {
                List<Template> loaded = await templateService.GetAllAsync();

                if (loaded.Count == 0)
                    return;

                loaded = loaded.Where(t => t.Company.Id == Site.Company.Id).ToList();
                templates = loaded;

                if (loaded.Count != 0)
                {
                    ChosenTemplate = loaded[0];
                }

                //ValidTemplates.Clear();
                //foreach (Item item in items)
                //{
                //    ValidItems.Add(item);
                //}
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
                item ??= new Item { Site = site };
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
                var response = await siteService.GenerateSite(Site, ChosenTemplate);

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

        private bool CanDelete()
        {
            return Client.Client.Instance.CurrentUser.IsAdmin;
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        private async Task DeleteSiteAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                await DeleteItemsAsync();
                var response = await siteService.DeleteAsync(Site.Id);

                if (response.Type == ResponseTypes.Ok)
                {
                    await Shell.Current.DisplayAlert("Внимание!", response.Message, "Oк");
                    await Shell.Current.Navigation.PopAsync(true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Oк");
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Oк");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteItemsAsync()
        {
            if (IsBusy) return;

            try
            {
                foreach (var item in items)
                {
                    IsBusy = true;
                    var response = await itemService.DeleteAsync(item.Id);

                    if (response.Type != ResponseTypes.Ok)
                    {
                        await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Oк");
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Oк");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
