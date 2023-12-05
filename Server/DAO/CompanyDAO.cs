using CMSLib.DTO;
using CMSLib.Model;

namespace Server.DAO
{
    internal class CompanyDAO : DAO<Company>
    {
        public override void Upsert(Company item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Companies.Update(item);
                db.SaveChanges();
            }
        }

        public override Company Get(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Companies.FirstOrDefault(c => c.Id == id);
            }
        }

        public Company GetByName(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Companies.FirstOrDefault(c => c.Name == name);
            }
        }

        public override List<Company> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Companies.ToList();
            }
        }

        public override void Remove(Company item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Companies.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
