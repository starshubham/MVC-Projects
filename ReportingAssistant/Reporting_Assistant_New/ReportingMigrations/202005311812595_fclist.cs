namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fclist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinalComments", "TaskDone_TaskDoneId", c => c.Long());
            CreateIndex("dbo.FinalComments", "TaskDone_TaskDoneId");
            AddForeignKey("dbo.FinalComments", "TaskDone_TaskDoneId", "dbo.TaskDones", "TaskDoneId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FinalComments", "TaskDone_TaskDoneId", "dbo.TaskDones");
            DropIndex("dbo.FinalComments", new[] { "TaskDone_TaskDoneId" });
            DropColumn("dbo.FinalComments", "TaskDone_TaskDoneId");
        }
    }
}
