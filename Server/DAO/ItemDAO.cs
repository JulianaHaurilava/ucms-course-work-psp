using CMSLib.DTO;
using CMSLib.Model;
using Microsoft.EntityFrameworkCore;

namespace Server.DAO
{
    internal class ItemDAO : DAO<Item>
    {
        public override void Upsert(Item item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Items.Update(item);
                db.SaveChanges();
            }
        }

        public override Item Get(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Items.FirstOrDefault(c => c.Id == id);
            }
        }

        public override List<Item> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Items.Include(c => c.Site).ThenInclude(s => s.Company).ToList();
            }
        }

        public override void Remove(Item item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Items.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
