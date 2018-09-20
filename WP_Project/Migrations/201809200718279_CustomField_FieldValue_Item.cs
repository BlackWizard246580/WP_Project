namespace WP_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomField_FieldValue_Item : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomFields",
                c => new
                    {
                        CustomFieldID = c.Int(nullable: false, identity: true),
                        CustomFieldName = c.String(),
                    })
                .PrimaryKey(t => t.CustomFieldID);
            
            CreateTable(
                "dbo.CustomFieldValues",
                c => new
                    {
                        CustomFieldValueID = c.Int(nullable: false, identity: true),
                        CustomFieldValueName = c.String(),
                        AdditionalPrice = c.Double(nullable: false),
                        CustomFieldID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomFieldValueID)
                .ForeignKey("dbo.CustomFields", t => t.CustomFieldID, cascadeDelete: true)
                .Index(t => t.CustomFieldID);
            
            CreateTable(
                "dbo.CustomFieldItems",
                c => new
                    {
                        CustomField_CustomFieldID = c.Int(nullable: false),
                        Item_ItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomField_CustomFieldID, t.Item_ItemID })
                .ForeignKey("dbo.CustomFields", t => t.CustomField_CustomFieldID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Item_ItemID, cascadeDelete: true)
                .Index(t => t.CustomField_CustomFieldID)
                .Index(t => t.Item_ItemID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomFieldItems", "Item_ItemID", "dbo.Items");
            DropForeignKey("dbo.CustomFieldItems", "CustomField_CustomFieldID", "dbo.CustomFields");
            DropForeignKey("dbo.CustomFieldValues", "CustomFieldID", "dbo.CustomFields");
            DropIndex("dbo.CustomFieldItems", new[] { "Item_ItemID" });
            DropIndex("dbo.CustomFieldItems", new[] { "CustomField_CustomFieldID" });
            DropIndex("dbo.CustomFieldValues", new[] { "CustomFieldID" });
            DropTable("dbo.CustomFieldItems");
            DropTable("dbo.CustomFieldValues");
            DropTable("dbo.CustomFields");
        }
    }
}
