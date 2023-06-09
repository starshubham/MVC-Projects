namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class list : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "TaskDone_TaskDoneId", c => c.Long());
            AddColumn("dbo.TaskDones", "TaskDone_TaskDoneId", c => c.Long());
            CreateIndex("dbo.Tasks", "TaskDone_TaskDoneId");
            CreateIndex("dbo.TaskDones", "TaskDone_TaskDoneId");
            AddForeignKey("dbo.TaskDones", "TaskDone_TaskDoneId", "dbo.TaskDones", "TaskDoneId");
            AddForeignKey("dbo.Tasks", "TaskDone_TaskDoneId", "dbo.TaskDones", "TaskDoneId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "TaskDone_TaskDoneId", "dbo.TaskDones");
            DropForeignKey("dbo.TaskDones", "TaskDone_TaskDoneId", "dbo.TaskDones");
            DropIndex("dbo.TaskDones", new[] { "TaskDone_TaskDoneId" });
            DropIndex("dbo.Tasks", new[] { "TaskDone_TaskDoneId" });
            DropColumn("dbo.TaskDones", "TaskDone_TaskDoneId");
            DropColumn("dbo.Tasks", "TaskDone_TaskDoneId");
        }
    }
}
