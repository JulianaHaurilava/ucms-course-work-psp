using CMSLib.DTO;
using CMSLib.Enum;
using CMSLib.TCP;
using Newtonsoft.Json;
using System.Diagnostics;

namespace UCMSApp.Services
{
    public class CompanyService
    {

        public async Task<Response> UpsertAsync(Company company)
        {
            try
            {
                var data = JsonConvert.SerializeObject(company);
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertCompany, data));
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
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteCompany, id.ToString()));
                var response = await Client.Client.Instance.GetResponseAsync();
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Company>> GetAllAsync()
        {
            try
            {
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetCompanies, ""));
                var response = await Client.Client.Instance.GetResponseAsync();
                var companies = JsonConvert.DeserializeObject<List<Company>>(response.Data);
                return companies;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
