using PrsCapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Utility;

namespace PrsCapstoneProject.Controllers
{
    public class VendorsController : Controller
    {
		private PrsDbContext db = new PrsDbContext();

		// GET: Vendors/List
		public ActionResult List() {
			return new JsonNetResult { Data = db.Vendors.ToList() };
		}

		// GET: Vendors/Get/5
		public ActionResult Get(int? id) {
			if (id == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id cannot be null." } };
			}
			var vendor = db.Vendors.Find(id);
			if (vendor == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id not found." } };
			}
			return new JsonNetResult { Data = vendor };
		}

		// POST: Vendors/Create
		public ActionResult Create([FromBody] Vendor vendor) {
			if (!ModelState.IsValid) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = ModelState } };
			}
			db.Vendors.Add(vendor);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Created." } };
		}

		// POST: Vendors/Change
		public ActionResult Change([FromBody] Vendor vendor) {
			if (!ModelState.IsValid) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = ModelState } };
			}
			var vendor2 = db.Vendors.Find(vendor.Id);
			vendor2.Copy(vendor);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Changed." } };
		}

		// POST: Vendors/Remove
		public ActionResult Remove([FromBody] Vendor vendor) {
			if (!ModelState.IsValid) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = ModelState } };
			}
			var vendor2 = db.Vendors.Find(vendor.Id);
			db.Vendors.Remove(vendor2);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Removed." } };
		}

	}
}