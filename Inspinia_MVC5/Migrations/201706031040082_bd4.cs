namespace Inspinia_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SSes",
                c => new
                    {
                        SociID = c.Int(nullable: false, identity: true),
                        NOM = c.String(nullable: false),
                        CODE_ACCES = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SociID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SSes");
        }
    }
}
