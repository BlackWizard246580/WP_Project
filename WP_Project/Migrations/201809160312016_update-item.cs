namespace WP_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateitem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Item_ItemID", "dbo.Items");
            DropIndex("dbo.Categories", new[] { "Item_ItemID" });
            AddColumn("dbo.Items", "Category_CategoryID", c => c.Int());
            CreateIndex("dbo.Items", "Category_CategoryID");
            AddForeignKey("dbo.Items", "Category_CategoryID", "dbo.Categories", "CategoryID");
            DropColumn("dbo.Categories", "Item_ItemID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Item_ItemID", c => c.Int());
            DropForeignKey("dbo.Items", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "Category_CategoryID" });
            DropColumn("dbo.Items", "Category_CategoryID");
            CreateIndex("dbo.Categories", "Item_ItemID");
            AddForeignKey("dbo.Categories", "Item_ItemID", "dbo.Items", "ItemID");
        }
    }
}
