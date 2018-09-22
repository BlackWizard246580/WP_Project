namespace WP_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _ItemImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Image");
        }
    }
}
