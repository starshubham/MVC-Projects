using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Reporting_Assistant_New.Migrations;
using Reporting_Assistant_New.ViewModel;
using Reporting_Assistant_New.Areas.ViewModels;

namespace Reporting_Assistant_New.Models
{
    public class ReportingDbContext:DbContext
    {
        public ReportingDbContext(): base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReportingDbContext,ReportingMigrations.Configuration>());
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<TaskDone> TaskDone { get; set; }
        public DbSet<FinalComments> FinalComment { get; set; }

    }
}