using Server.DAO;
using CMSLib.DTO;

namespace Server.Services
{
	public class UserService : Service<User>
	{
		private UserDAO dao = new UserDAO();
		public override void Upsert(User item)
		{
			dao.Upsert(item);
		}

		public override User Get(int id)
		{
			return dao.Get(id);
		}

		public override List<User> GetAll()
		{
			return dao.GetAll();
		}

		public override void Remove(User item)
		{
			dao.Remove(item);
		}
	}
}
