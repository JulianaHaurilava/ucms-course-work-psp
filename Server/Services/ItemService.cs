using CMSLib.DTO;
using Server.DAO;

namespace Server.Services
{
    public class ItemService : Service<Item>
    {
        private ItemDAO dao = new ItemDAO();
        public override void Upsert(Item item)
        {
            dao.Upsert(item);
        }

        public override Item Get(int id)
        {
            return dao.Get(id);
        }

        public override List<Item> GetAll()
        {
            return dao.GetAll();
        }

        public override void Remove(Item item)
        {
            dao.Remove(item);
        }
    }
}
