namespace GuiaApp.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuiaApp.Model.BDContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        
        }

        protected override void Seed(GuiaApp.Model.BDContext context)
        {
            //  This method will be called after migrating to the latest version.

            //City city = new City() { Description = "Delfinópolis", Uf = "MG" };
            //Menu op1 = new Menu() { Description = "Hospedagem", Active = true };
            //Menu op2 = new Menu() { Description = "Alimentação", Active = true };
            //Menu op3 = new Menu() { Description = "Atrativos", Active = true };
            //Menu op4 = new Menu() { Description = "Telefones úteis", Active = true };
            //User user = new User() { Email = "admin@admin.com.br", Password = "123456" };

            //context.City.Add(city);
            //context.Menu.Add(op1);
            //context.Menu.Add(op2);
            //context.Menu.Add(op3);
            //context.Menu.Add(op4);
            //context.User.Add(user);


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
