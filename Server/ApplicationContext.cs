using CMSLib.DTO;
using Microsoft.EntityFrameworkCore;

namespace CMSLib.Model
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Site> Sites { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Template> Templates { get; set; } = null!;


        public void Init()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();

            //var company = new Company { Name = "Company_1", Address = "Address_1" };

            //Users.Add(new User { Password = "", IsAdmin = true, Email = "", Company = company });
            //Users.Add(new User { Password = "admin", IsAdmin = true, Email = "admin@gmail.com", Company = company });
            //Users.Add(new User { Password = "user", IsAdmin = false, Email = "user@gmail.com" });

            //var template = new Template
            //{
            //    Name = "Custom",
            //    Style = @"<style>
            //                 body {
            //                    font-family: 'Arial',
            //                    sans-serif;\r\n
            //                    background-color: #f0f0f0;
            //                    margin: 20px;
            //                    }
            //                 h1 {color: #333;}h2 {color: #555;}
            //                 div {border: 1px solid #ccc;padding: 10px;margin-bottom: 10px;background-color: #fff;}
            //                 h3 {color: #007bff;}p {color: #333;}
            //                 p.price {color: #28a745;}
            //             </style>",
            //    Company = company
            //};

            //var site = new Site { Name = "Site_1", Description = "Descr_1", Company = company };
            //var item = new Item { Name = "Item_1", Description = "Descr_1", Price = 1.1, Site = site };

            //Templates.Add(template);
            //Sites.Add(site);
            //Items.Add(item);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cms_db;Username=postgres;Password=root;IncludeErrorDetail=True;")
            .EnableDetailedErrors();
        }
    }
}
