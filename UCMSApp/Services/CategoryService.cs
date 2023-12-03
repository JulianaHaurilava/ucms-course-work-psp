using CMSLib.DTO;
using CMSLib.Enum;
using CMSLib.TCP;
using Newtonsoft.Json;
using System.Diagnostics;

namespace UCMSApp.Services
{
    public class CategoryService /*IClientService<Site>*/
    {
        public async Task<Response> AuthorizeAsync(Category category)
        {
            try
            {
                var data = JsonConvert.SerializeObject(category);
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.Login, data));
                var response = await Client.Client.Instance.GetResponseAsync();
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Response> UpsertAsync(Category category)
        {
            try
            {
                var data = JsonConvert.SerializeObject(category);
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertCategory, data));
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
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteCategory, id.ToString()));
                var response = await Client.Client.Instance.GetResponseAsync();
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetCategories, ""));
                var response = await Client.Client.Instance.GetResponseAsync();
                var categories = JsonConvert.DeserializeObject<List<Category>>(response.Data);
                return categories;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
