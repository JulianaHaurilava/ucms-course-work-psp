using CMSLib.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UCMSApp.Services;
using UCMSApp.VVM.Base;

namespace UCMSApp.VVM.Templates
{
    public enum TemplatesOrderType
    {
        NameFromAToZ,
        NameFromZToA
    }
    public partial class TemplatesViewModel : BaseViewModel
    {
        public ObservableCollection<Template> ValidTemplates { get; private set; } = new();

        [ObservableProperty]
        private string searchString;
        [ObservableProperty]
        private List<string> orderTypesList = new() { "От А до Я", "От Я до А" };
        [ObservableProperty]
        private int orderTypeId = (int)TemplatesOrderType.NameFromAToZ;

        private List<Template> templates;
        private TemplateService service;

        public TemplatesViewModel(TemplateService service)
        {
            Title = "Шаблоны";
            this.service = service;
        }

        [RelayCommand]
        public async Task LoadElementsAsync()
        {
            try
            {
                List<Template> loaded = await service.GetAllAsync();

                if (loaded.Count == 0)
                    return;

                templates = loaded.Where(t => t.CompanyId == Client.Client.Instance.CurrentUser.Company.Id).ToList();
                ValidateElements();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
            }
        }



        [RelayCommand]
        private async Task EditTemplateAsync(Template template)
        {
            try
            {
                IsBusy = true;
                template ??= new Template { Company = Client.Client.Instance.CurrentUser.Company };
                if (template.Id == 0)
                {
                    await GoToAddTemplateWindow(template);
                    return;
                }
                await GoToEditTemplateWindow(template);
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Ок"); }
            finally { IsBusy = false; }
        }

        private async Task GoToEditTemplateWindow(Template template)
        {
            await Shell.Current.GoToAsync(nameof(EditTemplate), true, new Dictionary<string, object>()
            {
                {"Template", template}
            });
        }

        private async Task GoToAddTemplateWindow(Template template)
        {
            await Shell.Current.GoToAsync(nameof(AddTemplate), true, new Dictionary<string, object>()
            {
                {"Template", template}
            });
        }


        [RelayCommand]
        private void ValidateElements()
        {
            List<Template> searched = SearchElements(templates);
            var searchedAndOrdered = OrderElements(searched);

            ValidTemplates.Clear();
            foreach (Template template in searchedAndOrdered)
            {
                ValidTemplates.Add(template);
            }
        }

        private List<Template> SearchElements(List<Template> unsearched)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return unsearched;
            }

            List<Template> searched = new();
            searched = unsearched.Where(f => f.Name.Contains(SearchString)).ToList();
            return searched;
        }

        private List<Template> OrderElements(List<Template> unordered)
        {
            List<Template> ordered = new();
            var orderType = (SiteOrderType)OrderTypeId;
            switch (orderType)
            {
                case SiteOrderType.NameFromAToZ:
                    ordered = unordered.OrderBy((f) => f.Name).ToList();
                    break;
                case SiteOrderType.NameFromZToA:
                    ordered = unordered.OrderByDescending((f) => f.Name).ToList();
                    break;
            }
            return ordered;
        }
    }
}

