using CMSLib.DTO;
using CMSLib.Model;
using Microsoft.EntityFrameworkCore;

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

                File.WriteAllText(filePath, GenerateHTML(site.Name, site.Description));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании папки: {ex.Message}");
            }
        }

        private string GenerateHTML(string name, string description)
        {
            string html = $@"<!DOCTYPE html>
                    <html>
                    <head>
                        <title>{name}</title>
                    </head>
                    <body>
                        <h1>{name}</h1>
                        <p>{description}</p>
                    </body>
                    </html>";
            return html;
        }
    }
}
