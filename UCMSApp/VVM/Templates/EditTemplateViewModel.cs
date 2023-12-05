using CMSLib.DTO;
using CMSLib.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Templates
{
    [QueryProperty("Template", "Template")]
    public partial class EditTemplateViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Template template;

        private TemplateService service;

        public EditTemplateViewModel(TemplateService service)
        {
            this.service = service;
            Title = "Работа с шаблоном";
        }


        [RelayCommand]
        private async Task UpsertTemplateAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var response = await service.UpsertAsync(Template);

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

        [RelayCommand]
        public async Task DeleteTemplateAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var response = await service.DeleteAsync(Template.Id);

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
    }
}
