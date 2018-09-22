namespace WP_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _Order_OrderDetail_OrderDetailCustomField : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        SubTotalAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailID)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDateTime = c.DateTime(nullable: false),
                        OrderCreateDateTime = c.DateTime(nullable: false),
                        DeliverAddress = c.String(),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.OrderDetailCustomFields",
                c => new
                    {
                        OrderDetailCustomFieldID = c.Int(nullable: false, identity: true),
                        OrderDetailID = c.Int(nullable: false),
                        CustomFieldName = c.String(),
                        CustomFieldValueName = c.String(),
                        AdditionalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailCustomFieldID)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetailID, cascadeDelete: true)
                .Index(t => t.OrderDetailID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetailCustomFields", "OrderDetailID", "dbo.OrderDetails");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "ItemID", "dbo.Items");
            DropIndex("dbo.OrderDetailCustomFields", new[] { "OrderDetailID" });
            DropIndex("dbo.OrderDetails", new[] { "ItemID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropTable("dbo.OrderDetailCustomFields");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
