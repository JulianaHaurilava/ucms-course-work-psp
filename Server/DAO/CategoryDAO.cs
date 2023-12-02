using CMSLib.DTO;
using CMSLib.Model;

namespace Server.DAO
{
    internal class CategoryDAO : DAO<Category>
    {
        public override void Upsert(Category item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var category = Get(item.Id);
                if (category != null)
                {
                    db.Categories.Update(item);
                }
                else
                {
                    db.Categories.Add(item);
                }

                db.SaveChanges();
            }
        }

        public override Category Get(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Categories.FirstOrDefault(c => c.Id == id);
            }
        }

        public override List<Category> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Categories.ToList();
            }
        }

        public override void Remove(Category item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Categories.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
