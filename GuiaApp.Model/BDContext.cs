using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiaApp.Model
{
    public class BDContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Local> Local { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CityMenu> CityMenu { get; set; }
        public DbSet<Image> Image { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Configuration.LazyLoadingEnabled = false;
        }
    }
}
