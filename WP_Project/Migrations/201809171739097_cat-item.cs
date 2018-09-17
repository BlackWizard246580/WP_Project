namespace WP_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catitem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomFields", "CustomFieldValue_CustomFieldValueID", "dbo.CustomFieldValues");
            DropForeignKey("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ItemID" }, "dbo.OrderDetails");
            DropForeignKey("dbo.Items", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.CustomFields", new[] { "CustomFieldValue_CustomFieldValueID" });
            DropIndex("dbo.Items", new[] { "Category_CategoryID" });
            DropIndex("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ItemID" });
            RenameColumn(table: "dbo.Items", name: "Category_CategoryID", newName: "CategoryID");
            AlterColumn("dbo.Items", "ItemPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Items", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "CategoryID");
            AddForeignKey("dbo.Items", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
            DropTable("dbo.CustomFields");
            DropTable("dbo.CustomFieldValues");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ItemID });
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDateTime = c.DateTime(nullable: false),
                        OrderCreateDateTime = c.DateTime(nullable: false),
                        DeliverAddress = c.String(),
                        Total = c.Double(nullable: false),
                        OrderDetail_OrderID = c.Int(),
                        OrderDetail_ItemID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.CustomFieldValues",
                c => new
                    {
                        CustomFieldValueID = c.Int(nullable: false, identity: true),
                        CustomFieldValueName = c.String(),
                        CustomFieldID = c.Int(nullable: false),
                        AdditionalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CustomFieldValueID);
            
            CreateTable(
                "dbo.CustomFields",
                c => new
                    {
                        CustomFieldID = c.Int(nullable: false, identity: true),
                        CustomFieldName = c.String(),
                        CustomFieldValue_CustomFieldValueID = c.Int(),
                    })
                .PrimaryKey(t => t.CustomFieldID);
            
            DropForeignKey("dbo.Items", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "CategoryID" });
            AlterColumn("dbo.Items", "CategoryID", c => c.Int());
            AlterColumn("dbo.Items", "ItemPrice", c => c.String());
            RenameColumn(table: "dbo.Items", name: "CategoryID", newName: "Category_CategoryID");
            CreateIndex("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ItemID" });
            CreateIndex("dbo.Items", "Category_CategoryID");
            CreateIndex("dbo.CustomFields", "CustomFieldValue_CustomFieldValueID");
            AddForeignKey("dbo.Items", "Category_CategoryID", "dbo.Categories", "CategoryID");
            AddForeignKey("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ItemID" }, "dbo.OrderDetails", new[] { "OrderID", "ItemID" });
            AddForeignKey("dbo.CustomFields", "CustomFieldValue_CustomFieldValueID", "dbo.CustomFieldValues", "CustomFieldValueID");
        }
    }
}
