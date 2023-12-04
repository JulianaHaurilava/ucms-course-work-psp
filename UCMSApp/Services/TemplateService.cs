using CMSLib.DTO;
using CMSLib.Enum;
using CMSLib.TCP;
using Newtonsoft.Json;
using System.Diagnostics;

namespace UCMSApp.Services
{
    public class TemplateService
    {
        public async Task<Response> UpsertAsync(Template template)
        {
            try
            {
                var data = JsonConvert.SerializeObject(template);
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertTemplate, data));
                var response = await Client.Client.Instance.GetResponseAsync();
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteTemplate, id.ToString()));
                var response = await Client.Client.Instance.GetResponseAsync();
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Template>> GetAllAsync()
        {
            try
            {
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetTemplates, ""));
                var response = await Client.Client.Instance.GetResponseAsync();
                var templates = JsonConvert.DeserializeObject<List<Template>>(response.Data);
                return templates;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
