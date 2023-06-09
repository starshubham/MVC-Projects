namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsernameFinalComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskDones", "adminid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskDones", "adminid");
        }
    }
}
