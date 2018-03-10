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
			var user = new User(0, "sa", "as", "Super", "Admin", "", "", true, true, true);
			context.Users.AddOrUpdate(
			);
        }
    }
}
