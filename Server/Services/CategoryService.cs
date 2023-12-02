using CMSLib.DTO;
using Server.DAO;

namespace Server.Services
{
    public class CategoryService : Service<Category>
    {
        private CategoryDAO dao = new CategoryDAO();
        public override void Upsert(Category item)
        {
            dao.Upsert(item);
        }

        public override Category Get(int id)
        {
            return dao.Get(id);
        }

        public override List<Category> GetAll()
        {
            return dao.GetAll();
        }

        public override void Remove(Category item)
        {
            dao.Remove(item);
        }
    }
}
