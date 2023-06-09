namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image_not_mandatory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "image", c => c.String(nullable: false));
        }
    }
}
