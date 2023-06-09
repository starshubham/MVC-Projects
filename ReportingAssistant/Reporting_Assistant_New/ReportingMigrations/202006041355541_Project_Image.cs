namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Project_Image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "image");
        }
    }
}
