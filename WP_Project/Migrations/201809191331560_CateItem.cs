namespace WP_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CateItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemPrice = c.Double(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "CategoryID" });
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}
