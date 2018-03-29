using PrsCapstoneProject.Models;
using PrsCapstoneProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Utility;

namespace PrsCapstoneProject.Controllers
{
	[AllowCrossSiteJson]
	public class ProductsController : Controller
    {
		private PrsDbContext db = new PrsDbContext();

		// GET: Products/List
		public ActionResult List() {
			return new JsonNetResult { Data = db.Products.ToList() };
		}

		// GET: Products/Get/5
		public ActionResult Get(int? id) {
			if (id == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id cannot be null." } };
			}
			var product = db.Products.Find(id);
			if (product == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id not found." } };
			}
			return new JsonNetResult { Data = product };
		}

		// POST: Products/Create
		public ActionResult Create([FromBody] Product product) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			db.Products.Add(product);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Created." } };
		}

		// POST: Products/Change
		public ActionResult Change([FromBody] Product product) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			var product2 = db.Products.Find(product.Id);
			product2.Copy(product);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Changed." } };
		}

		// POST: Products/Remove
		public ActionResult Remove([FromBody] Product product) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			var product2 = db.Products.Find(product.Id);
			db.Products.Remove(product2);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Removed." } };
		}
	}
}