namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FinalComments", "AdminUserId", c => c.String());
            AlterColumn("dbo.FinalComments", "UserId", c => c.String());
            AlterColumn("dbo.Tasks", "AdminUserId", c => c.String());
            AlterColumn("dbo.Tasks", "UserId", c => c.String());
            AlterColumn("dbo.TaskDones", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskDones", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Tasks", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Tasks", "AdminUserId", c => c.Long(nullable: false));
            AlterColumn("dbo.FinalComments", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.FinalComments", "AdminUserId", c => c.Long(nullable: false));
        }
    }
}
