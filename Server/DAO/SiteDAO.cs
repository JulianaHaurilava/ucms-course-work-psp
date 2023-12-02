using CMSLib.DTO;
using CMSLib.Model;

namespace Server.DAO
{
    internal class SiteDAO : DAO<Site>
    {
        public override void Upsert(Site item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var site = Get(item.Id);
                if (site != null)
                {
                    db.Sites.Update(item);
                }
                else
                {
                    db.Sites.Add(item);
                }

                db.SaveChanges();
            }
        }

        public override Site Get(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Sites.FirstOrDefault(c => c.Id == id);
            }
        }

        public override List<Site> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Sites.ToList();
            }
        }

        public override void Remove(Site item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Sites.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
