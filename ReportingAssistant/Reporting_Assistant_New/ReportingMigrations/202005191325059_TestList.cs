namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Task_TaskId", c => c.Long());
            CreateIndex("dbo.Tasks", "Task_TaskId");
            AddForeignKey("dbo.Tasks", "Task_TaskId", "dbo.Tasks", "TaskId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Task_TaskId", "dbo.Tasks");
            DropIndex("dbo.Tasks", new[] { "Task_TaskId" });
            DropColumn("dbo.Tasks", "Task_TaskId");
        }
    }
}
