using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PrsCapstoneProject.Models {

	public class PrsDbContext : DbContext {

		public PrsDbContext() : base() { }

		public DbSet<User> Users { get; set; }
	}
}