namespace Reporting_Assistant_New.ReportingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.FinalComments",
                c => new
                    {
                        FinalCommentId = c.Long(nullable: false, identity: true),
                        Screen = c.String(),
                        Description = c.String(),
                        AdminUserId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        DateOfFinalComment = c.DateTime(),
                        ImageAttached = c.String(),
                        ProjectId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.FinalCommentId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                        DateOfStart = c.DateTime(),
                        Availability = c.Boolean(nullable: false),
                        CategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Long(nullable: false, identity: true),
                        Screen = c.String(),
                        Description = c.String(),
                        AdminUserId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        DateOfTask = c.DateTime(),
                        ImageAttached = c.String(),
                        ProjectId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.TaskDones",
                c => new
                    {
                        TaskDoneId = c.Long(nullable: false, identity: true),
                        Screen = c.String(),
                        Description = c.String(),
                        UserId = c.Long(nullable: false),
                        DateOfTaskDone = c.DateTime(),
                        ImageAttached = c.String(),
                        ProjectId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TaskDoneId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskDones", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.FinalComments", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CategoryId", "dbo.Categories");
            DropIndex("dbo.TaskDones", new[] { "ProjectId" });
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "CategoryId" });
            DropIndex("dbo.FinalComments", new[] { "ProjectId" });
            DropTable("dbo.TaskDones");
            DropTable("dbo.Tasks");
            DropTable("dbo.Projects");
            DropTable("dbo.FinalComments");
            DropTable("dbo.Categories");
        }
    }
}
