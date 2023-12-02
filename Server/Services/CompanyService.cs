using Server.DAO;
using CMSLib.DTO;

namespace Server.Services
{
	public class CompanyService : Service<Company>
	{
		private CompanyDAO dao = new CompanyDAO();
		public override void Upsert(Company item)
		{
			dao.Upsert(item);
		}

		public override Company Get(int id)
		{
			return dao.Get(id);
		}

		public override List<Company> GetAll()
		{
			return dao.GetAll();
		}

		public override void Remove(Company item)
		{
			dao.Remove(item);
		}
	}
}
