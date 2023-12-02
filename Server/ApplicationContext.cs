using Microsoft.EntityFrameworkCore;
using CMSLib.DTO;

namespace CMSLib.Model
{
	class ApplicationContext : DbContext
	{
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Company> Companies { get; set; } = null!;
		public DbSet<Site> Sites { get; set; } = null!;
		public DbSet<Category> Categories { get; set; } = null!;
		public DbSet<Item> Items { get; set; } = null!;


		public void Init()
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();

			Users.Add(new User { Login = "", Password = "", IsAdmin = true, Email = "@gmail.com" });
			Users.Add(new User { Login = "admin", Password = "admin", IsAdmin = true, Email = "admin@gmail.com" });
			Users.Add(new User { Login = "user", Password = "user", IsAdmin = false, Email = "user@gmail.com" });

			var company = new Company { Name = "Company_1", Address = "Address_1" };
			var site = new Site { Name = "Site_1", Description = "Descr_1", Company = company };
			var category = new Category { Name = "Category_1", Description = "", Site = site };
			var item = new Item { Name = "Item_1", Description = "Descr_1", Price = 1.1, Category = category };

			Companies.Add(company);
			Sites.Add(site);
			Categories.Add(category);
			Items.Add(item);
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cms_db;Username=postgres;Password=root;IncludeErrorDetail=True;")
			.EnableDetailedErrors();
		}
	}
}
