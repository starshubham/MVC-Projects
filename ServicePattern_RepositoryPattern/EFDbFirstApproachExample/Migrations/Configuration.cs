namespace EFDbFirstApproachExample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Company.DataLayer.CompanyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Company.DataLayer.CompanyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Brands.AddOrUpdate(new Company.DomainModels.Brand() { BrandID = 1, BrandName = "Samsung" }, new Company.DomainModels.Brand() { BrandID = 2, BrandName = "Apple" }, new Company.DomainModels.Brand() { BrandID = 3, BrandName = "Sony" }, new Company.DomainModels.Brand() { BrandID = 4, BrandName = "HP" });
            context.Categories.AddOrUpdate(new Company.DomainModels.Category() { CategoryID = 1, CategoryName = "Electronics" }, new Company.DomainModels.Category() { CategoryID = 2, CategoryName = "Home Appliances" });
            context.Products.AddOrUpdate(new Company.DomainModels.Product() { ProductID = 1, ProductName = "Samsung Galaxy Mobile", CategoryID = 1, BrandID = 1, Price = 10000, Photo = null, Active = true, AvailabilityStatus = "InStock", DOP = DateTime.Now });
        }
    }
}
