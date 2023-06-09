namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UseAndAdminrame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "username", c => c.String());
            AddColumn("dbo.Tasks", "AdminName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "AdminName");
            DropColumn("dbo.Tasks", "username");
        }
    }
}
