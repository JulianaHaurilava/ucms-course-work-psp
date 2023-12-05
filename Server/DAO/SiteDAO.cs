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
                db.Sites.Update(item);
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
                return db.Sites.Include(s => s.Company).ToList();
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

        public void GenerateSite(Site site, Template template)
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

                File.WriteAllText(filePath, GenerateHTML(site, template));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании папки: {ex.Message}");
            }
        }

        private string GenerateHTML(Site site, Template template)
        {
            string html = string.Format(template.HeaderCode[0], site.Name);

            if (!string.IsNullOrEmpty(template.Style) )
            {
                html += template.Style;
            }
            html += string.Format(template.HeaderCode[1], site.Name, site.Description); ;

            ItemService itemService = new();
            List<Item> items = itemService.GetAll();

            foreach (Item item in items) 
            {
                if (item.Site.Id == site.Id)
                {
                    html += string.Format(template.ItemsCode, item.Name, item.Description, item.Price);
                }
            }
            html += template.EndCode;

            return html;
        }
    }
}
