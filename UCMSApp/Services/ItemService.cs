using CMSLib.DTO;
using CMSLib.Enum;
using CMSLib.TCP;
using Newtonsoft.Json;
using System.Diagnostics;

namespace UCMSApp.Services
{
    public class ItemService /*IClientService<Site>*/
    {
        public async Task<Response> AuthorizeAsync(Item item)
        {
            try
            {
                var data = JsonConvert.SerializeObject(item);
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

        public async Task<Response> UpsertAsync(Item item)
        {
            try
            {
                var data = JsonConvert.SerializeObject(item);
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertItem, data));
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
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteItem, id.ToString()));
                var response = await Client.Client.Instance.GetResponseAsync();
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Item>> GetAllAsync()
        {
            try
            {
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetItems, ""));
                var response = await Client.Client.Instance.GetResponseAsync();
                var items = JsonConvert.DeserializeObject<List<Item>>(response.Data);
                return items;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
