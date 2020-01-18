namespace Inspinia_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tiers",
                c => new
                    {
                        TiersID = c.Int(nullable: false, identity: true),
                        SociID = c.Int(nullable: false),
                        TEL = c.String(),
                        FAX = c.String(),
                        CONTACT = c.String(),
                        MOBILE = c.String(),
                        ADRESSE = c.String(),
                        MAIL = c.String(),
                        MODALITE_PAYEMENT = c.String(),
                        act_ID = c.Int(nullable: false),
                        TYPE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TiersID)
                .ForeignKey("dbo.Acs", t => t.act_ID, cascadeDelete: true)
                .ForeignKey("dbo.SSes", t => t.SociID, cascadeDelete: true)
                .Index(t => t.SociID)
                .Index(t => t.act_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tiers", "SociID", "dbo.SSes");
            DropForeignKey("dbo.Tiers", "act_ID", "dbo.Acs");
            DropIndex("dbo.Tiers", new[] { "act_ID" });
            DropIndex("dbo.Tiers", new[] { "SociID" });
            DropTable("dbo.Tiers");
        }
    }
}
