using CMSLib.DTO;
using CMSLib.Enum;
using CMSLib.TCP;
using Newtonsoft.Json;
using System.Diagnostics;

namespace UCMSApp.Services
{
    public enum UserOrderType
    {
        NameFromAToZ,
        NameFromZToA,
    }

    public class UserService  /*IClientService<User>*/
    {
        public async Task<Response> AuthorizeAsync(User user)
        {
            try
            {
                var data = JsonConvert.SerializeObject(user);
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

        public async Task<Response> UpsertAsync(User user)
        {
            try
            {
                var data = JsonConvert.SerializeObject(user);
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertUser, data));
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
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteUser, id.ToString()));
                var response = await Client.Client.Instance.GetResponseAsync();
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetUsers, ""));
                var response = await Client.Client.Instance.GetResponseAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(response.Data);
                return users;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
