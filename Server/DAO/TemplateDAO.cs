using CMSLib.DTO;
using CMSLib.Model;
using Microsoft.EntityFrameworkCore;

namespace Server.DAO
{
    internal class TemplateDAO : DAO<Template>
    {
        public override void Upsert(Template item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var template = Get(item.Id);
                if (template != null)
                {
                    db.Templates.Update(item);
                }
                else
                {
                    db.Templates.Add(item);
                }

                db.SaveChanges();
            }
        }

        public override Template Get(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Templates.FirstOrDefault(c => c.Id == id);
            }
        }

        public override List<Template> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Templates.Include(t => t.Company).ToList();
            }
        }

        public override void Remove(Template item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Templates.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
