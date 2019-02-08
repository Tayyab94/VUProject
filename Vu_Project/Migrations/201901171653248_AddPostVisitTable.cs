namespace Vu_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostVisitTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostVisits",

                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        VistTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostVisits", "PostId", "dbo.Posts");
            DropIndex("dbo.PostVisits", new[] { "PostId" });
            DropTable("dbo.PostVisits");
        }
    }
}
