namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Required_Image : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "image", c => c.String());
        }
    }
}
