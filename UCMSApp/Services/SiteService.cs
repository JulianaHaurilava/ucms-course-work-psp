using CMSLib.DTO;
using CMSLib.Enum;
using CMSLib.TCP;
using Newtonsoft.Json;
using System.Diagnostics;

namespace UCMSApp.Services
{
    public enum SiteOrderType
    {
        NameFromAToZ,
        NameFromZToA,
    }
    public class SiteService /*IClientService<Site>*/
    {
        public async Task<Response> UpsertAsync(Site site)
        {
            try
            {
                var data = JsonConvert.SerializeObject(site);
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertSite, data));
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
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteSite, id.ToString()));
                var response = await Client.Client.Instance.GetResponseAsync();
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Site>> GetAllAsync()
        {
            try
            {
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetSites, ""));
                var response = await Client.Client.Instance.GetResponseAsync();
                var sites = JsonConvert.DeserializeObject<List<Site>>(response.Data);
                return sites;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Response> GenerateSite(Site site, Template template)
        {
            try
            {
                TemplatedSite templatedSite = new TemplatedSite { Template = template, Site = site };
                var data = JsonConvert.SerializeObject(templatedSite);
                await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GenerateSite, data));
                var response = await Client.Client.Instance.GetResponseAsync();
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
