using CMSLib.Model;
using CMSLib.DTO;
using Microsoft.EntityFrameworkCore;

namespace Server.DAO
{
	public class UserDAO : DAO<User>
	{
		public override void Upsert(User user)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				//var userInDB = Get(user.Id);
				//if (userInDB != null)
				//{
					db.Users.Update(user);
				//}
				//else
				//{
				//	db.Users.Add(user);
				//}
				db.SaveChanges();
			}
		}

		public override User Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Users.FirstOrDefault(u => u.Id == id);
			}
		}

		public override List<User> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Users.Include(u => u.Company).ToList();
			}
		}

		public override void Remove(User user)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Users.Remove(user);
				db.SaveChanges();
			}
		}
	}
}
