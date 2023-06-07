namespace EFDbFirstApproachExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingNullColumnFromActive : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Active", c => c.Boolean());
        }
    }
}
