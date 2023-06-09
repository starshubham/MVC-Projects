namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "ProjectName", c => c.String());
            DropColumn("dbo.Projects", "CategoryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "CategoryName", c => c.String());
            DropColumn("dbo.Projects", "ProjectName");
        }
    }
}
