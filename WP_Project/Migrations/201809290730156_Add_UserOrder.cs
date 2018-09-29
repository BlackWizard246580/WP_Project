namespace WP_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserID", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            CreateIndex("dbo.Orders", "UserID");
            AddForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.Orders", "UserID");
        }
    }
}
