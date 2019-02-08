namespace Vu_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTimeAndapproavedPropertyIntoPostTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "Approaved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Approaved");
            DropColumn("dbo.Posts", "PostDate");
        }
    }
}
