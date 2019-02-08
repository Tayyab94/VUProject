namespace Vu_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImgUrlIntOPostTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ImgUrl");
        }
    }
}
