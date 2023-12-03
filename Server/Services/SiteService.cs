using CMSLib.DTO;
using Server.DAO;

namespace Server.Services
{
    public class SiteService : Service<Site>
    {
        private SiteDAO dao = new SiteDAO();
        public override void Upsert(Site item)
        {
            dao.Upsert(item);
        }

        public override Site Get(int id)
        {
            return dao.Get(id);
        }

        public override List<Site> GetAll()
        {
            return dao.GetAll();
        }

        public override void Remove(Site item)
        {
            dao.Remove(item);
        }

        public void GenerateSite(Site site)
        {
            dao.GenerateSite(site);
        }
    }
}
