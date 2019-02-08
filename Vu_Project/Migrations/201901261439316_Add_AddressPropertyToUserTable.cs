namespace Vu_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AddressPropertyToUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
