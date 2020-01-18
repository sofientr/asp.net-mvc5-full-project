namespace Inspinia_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.S",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SociID = c.Int(nullable: false),
                        NOM = c.String(nullable: false),
                        CODE_ACCES = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.S");
        }
    }
}
