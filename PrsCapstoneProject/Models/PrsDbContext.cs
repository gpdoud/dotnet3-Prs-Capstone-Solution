using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PrsCapstoneProject.Models {

	public class PrsDbContext : DbContext {

		public PrsDbContext() : base() { }

		public DbSet<User> Users { get; set; }
		public DbSet<Vendor> Vendors { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
		public DbSet<PurchaseRequestLineitem> PurchaseRequestLineitems { get; set; }
	}
}