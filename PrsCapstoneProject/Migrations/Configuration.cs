namespace PrsCapstoneProject.Migrations
{
	using PrsCapstoneProject.Models;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PrsCapstoneProject.Models.PrsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PrsCapstoneProject.Models.PrsDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.

			//if (System.Diagnostics.Debugger.IsAttached == false)
			//	System.Diagnostics.Debugger.Launch();
			
			context.Users.AddOrUpdate(
				new User(0, "sa", "as", "Super", "Admin", "513-555-1212", "sa@admin.com", true, true, true)
			);
			context.Vendors.AddOrUpdate(
				new Vendor(0, "AMAZ", "Amazon", "123 Street", "AnyCity", "CA", "12345", "987-555-1212", "info@amazon.com", true, true)
			);
			var AmazonId = context.Vendors.Where(v => v.Code == "AMAZ").ToArray()[0].Id;
			context.Products.AddOrUpdate(
				new Product(0, AmazonId, "Echo", "Echo", 79.99m, "Each", null, true),
				new Product(0, AmazonId, "Echo Dot", "Echo Dot", 49.99m, "Each", null, true)
			);
        }
    }
}
