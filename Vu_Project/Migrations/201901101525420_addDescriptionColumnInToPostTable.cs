namespace Vu_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDescriptionColumnInToPostTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Description");
        }
    }
}
