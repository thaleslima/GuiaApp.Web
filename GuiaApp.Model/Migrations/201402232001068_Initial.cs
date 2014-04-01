namespace GuiaApp.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, unicode: false),
                        Uf = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CityMenu",
                c => new
                    {
                        IdCity = c.Int(nullable: false),
                        IdMenu = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdCity, t.IdMenu })
                .ForeignKey("dbo.City", t => t.IdCity, cascadeDelete: true)
                .ForeignKey("dbo.Menu", t => t.IdMenu, cascadeDelete: true)
                .Index(t => t.IdCity)
                .Index(t => t.IdMenu);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Single(nullable: false),
                        Desciption = c.String(unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        IdUser = c.Int(nullable: false),
                        IdLocal = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Local", t => t.IdLocal, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdLocal)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Local",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Telephone = c.String(unicode: false),
                        Site = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Latitude = c.String(unicode: false),
                        Longitude = c.String(unicode: false),
                        Active = c.Boolean(nullable: false),
                        PathImage = c.String(unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        IdMenu = c.Int(nullable: false),
                        IdCity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.IdCity, cascadeDelete: true)
                .ForeignKey("dbo.Menu", t => t.IdMenu, cascadeDelete: true)
                .Index(t => t.IdCity)
                .Index(t => t.IdMenu);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                        Name = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "IdUser", "dbo.User");
            DropForeignKey("dbo.Comment", "IdLocal", "dbo.Local");
            DropForeignKey("dbo.Local", "IdMenu", "dbo.Menu");
            DropForeignKey("dbo.Local", "IdCity", "dbo.City");
            DropForeignKey("dbo.CityMenu", "IdMenu", "dbo.Menu");
            DropForeignKey("dbo.CityMenu", "IdCity", "dbo.City");
            DropIndex("dbo.Comment", new[] { "IdUser" });
            DropIndex("dbo.Comment", new[] { "IdLocal" });
            DropIndex("dbo.Local", new[] { "IdMenu" });
            DropIndex("dbo.Local", new[] { "IdCity" });
            DropIndex("dbo.CityMenu", new[] { "IdMenu" });
            DropIndex("dbo.CityMenu", new[] { "IdCity" });
            DropTable("dbo.User");
            DropTable("dbo.Local");
            DropTable("dbo.Comment");
            DropTable("dbo.Menu");
            DropTable("dbo.CityMenu");
            DropTable("dbo.City");
        }
    }
}
