using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Sites
{
    [QueryProperty("Site", "Site")]
    public partial class AddSiteViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Site site;

        private SiteService siteService;

        public AddSiteViewModel(SiteService siteService)
        {
            Title = "Создание сайта";
            this.siteService = siteService;
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
                    await Shell.Current.DisplayAlert("Внимание!", response.Message, "Ок");
                    await Shell.Current.Navigation.PopAsync(true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Ок");
                }

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
    }
}

