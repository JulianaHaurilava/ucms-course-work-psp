using CMSLib.DTO;
using Server.DAO;

namespace Server.Services
{
    public class TemplateService : Service<Template>
    {
        private TemplateDAO dao = new TemplateDAO();
        public override void Upsert(Template item)
        {
            dao.Upsert(item);
        }

        public override Template Get(int id)
        {
            return dao.Get(id);
        }

        public override List<Template> GetAll()
        {
            return dao.GetAll();
        }

        public override void Remove(Template item)
        {
            dao.Remove(item);
        }
    }
}
