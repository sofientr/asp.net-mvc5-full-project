namespace Inspinia_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bdt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CatID = c.Int(nullable: false, identity: true),
                        LIBELLE = c.String(),
                    })
                .PrimaryKey(t => t.CatID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DepartementID = c.Int(nullable: false),
                        Categories_CatID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Departements", t => t.DepartementID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Categories_CatID)
                .Index(t => t.DepartementID)
                .Index(t => t.Categories_CatID);
            
            CreateTable(
                "dbo.Departements",
                c => new
                    {
                        DepartementID = c.Int(nullable: false, identity: true),
                        DepartementName = c.String(),
                    })
                .PrimaryKey(t => t.DepartementID);
            
            CreateTable(
                "dbo.Centres_couts",
                c => new
                    {
                        CentreID = c.Int(nullable: false, identity: true),
                        LIBELLE = c.String(),
                        CatID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CentreID)
                .ForeignKey("dbo.Categories", t => t.CatID, cascadeDelete: true)
                .Index(t => t.CatID);
            
            CreateTable(
                "dbo.Projets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CODE = c.String(),
                        NOM_PROJET = c.String(nullable: false, maxLength: 35),
                        TYPE = c.String(),
                        CLIENT = c.Int(nullable: false),
                        OWNER = c.Int(nullable: false),
                        DEBUT = c.DateTime(nullable: false),
                        FIN = c.DateTime(nullable: false),
                        MONTANT_HT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TVA = c.Int(nullable: false),
                        GARANTIE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TYPE_FACTURATION = c.String(),
                        MODALITE_FACTURATION = c.String(),
                        REFERENCE = c.String(),
                        SOCIETE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Societes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NOM = c.String(nullable: false),
                        CODE_ACCES = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.String(),
                        Office = c.String(),
                        Extn = c.String(),
                        Salary = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Centres_couts", "CatID", "dbo.Categories");
            DropForeignKey("dbo.Employees", "Categories_CatID", "dbo.Categories");
            DropForeignKey("dbo.Employees", "DepartementID", "dbo.Departements");
            DropIndex("dbo.Centres_couts", new[] { "CatID" });
            DropIndex("dbo.Employees", new[] { "Categories_CatID" });
            DropIndex("dbo.Employees", new[] { "DepartementID" });
            DropTable("dbo.Workers");
            DropTable("dbo.Societes");
            DropTable("dbo.Projets");
            DropTable("dbo.Centres_couts");
            DropTable("dbo.Departements");
            DropTable("dbo.Employees");
            DropTable("dbo.Categories");
        }
    }
}
