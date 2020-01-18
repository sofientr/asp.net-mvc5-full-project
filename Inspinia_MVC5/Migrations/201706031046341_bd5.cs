namespace Inspinia_MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Acs",
                c => new
                    {
                        act_ID = c.Int(nullable: false, identity: true),
                        DESIGNATION = c.String(),
                    })
                .PrimaryKey(t => t.act_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Acs");
        }
    }
}
