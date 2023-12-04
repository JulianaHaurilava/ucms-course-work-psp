using CMSLib.DTO;
using CMSLib.Model;
using Microsoft.EntityFrameworkCore;
using Server.Services;

namespace Server.DAO
{
    internal class SiteDAO : DAO<Site>
    {
        public override void Upsert(Site item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //var site = Get(item.Id);
                //if (site != null)
                //{
                    db.Sites.Update(item);
                //}
                //else
                //{
                //    db.Sites.Add(item);
                //}

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
                return db.Sites.Include(s => s.Company).Include(s => s.Template).ToList();
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

        public void GenerateSite(Site site)
        {
            string folderPath = Directory.GetCurrentDirectory() + "/" + "sites";

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = site.Name + ".html";
                string filePath = Path.Combine(folderPath, fileName);

                File.WriteAllText(filePath, GenerateHTML(site));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании папки: {ex.Message}");
            }
        }

        private string GenerateHTML(Site site)
        {
            string html = string.Format(site.Template.HeaderCode, site.Name, site.Description);

            ItemService itemService = new();
            List<Item> items = itemService.GetAll();

            foreach (Item item in items) 
            {
                html += string.Format(site.Template.ItemsCode, item.Name, item.Description, item.Price);
            }
            html += site.Template.EndCode;

            return html;
        }
    }
}
