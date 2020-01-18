namespace Inspinia_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Socis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NOM = c.String(nullable: false),
                        CODE_ACCES = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Socis");
        }
    }
}
