namespace Inspinia_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actis",
                c => new
                    {
                        CatID = c.Int(nullable: false, identity: true),
                        DESIGNATION = c.String(),
                    })
                .PrimaryKey(t => t.CatID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Actis");
        }
    }
}
