namespace Inspinia_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prrs",
                c => new
                    {
                        PrID = c.Int(nullable: false, identity: true),
                        CODE = c.String(),
                        NOM_PROJET = c.String(),
                        TYPE = c.String(),
                        TiersID = c.Int(nullable: false),
                        DEBUT = c.DateTime(nullable: false),
                        FIN = c.DateTime(nullable: false),
                        MONTANT_HT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TVA = c.Int(nullable: false),
                        GARANTIE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TYPE_FACTURATION = c.String(),
                        MODALITE_FACTURATION = c.String(),
                        REFERENCE = c.String(),
                        SociID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PrID)
                .ForeignKey("dbo.SSes", t => t.SociID, cascadeDelete: false)
                .ForeignKey("dbo.Tiers", t => t.TiersID, cascadeDelete: false)
                .Index(t => t.TiersID)
                .Index(t => t.SociID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prrs", "TiersID", "dbo.Tiers");
            DropForeignKey("dbo.Prrs", "SociID", "dbo.SSes");
            DropIndex("dbo.Prrs", new[] { "SociID" });
            DropIndex("dbo.Prrs", new[] { "TiersID" });
            DropTable("dbo.Prrs");
        }
    }
}
