namespace WP_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBContext1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Item_ItemID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Items", t => t.Item_ItemID)
                .Index(t => t.Item_ItemID);
            
            CreateTable(
                "dbo.CustomFields",
                c => new
                    {
                        CustomFieldID = c.Int(nullable: false, identity: true),
                        CustomFieldName = c.String(),
                        CustomFieldValue_CustomFieldValueID = c.Int(),
                    })
                .PrimaryKey(t => t.CustomFieldID)
                .ForeignKey("dbo.CustomFieldValues", t => t.CustomFieldValue_CustomFieldValueID)
                .Index(t => t.CustomFieldValue_CustomFieldValueID);
            
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
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemPrice = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
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
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.OrderDetails", t => new { t.OrderDetail_OrderID, t.OrderDetail_ItemID })
                .Index(t => new { t.OrderDetail_OrderID, t.OrderDetail_ItemID });
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ItemID" }, "dbo.OrderDetails");
            DropForeignKey("dbo.Categories", "Item_ItemID", "dbo.Items");
            DropForeignKey("dbo.CustomFields", "CustomFieldValue_CustomFieldValueID", "dbo.CustomFieldValues");
            DropIndex("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ItemID" });
            DropIndex("dbo.CustomFields", new[] { "CustomFieldValue_CustomFieldValueID" });
            DropIndex("dbo.Categories", new[] { "Item_ItemID" });
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Items");
            DropTable("dbo.CustomFieldValues");
            DropTable("dbo.CustomFields");
            DropTable("dbo.Categories");
        }
    }
}
