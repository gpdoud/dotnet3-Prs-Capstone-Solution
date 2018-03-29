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
	public class PurchaseRequestLineitemsController : Controller
    {
		private PrsDbContext db = new PrsDbContext();

		private void CalculatePurchaseRequestTotal(int prId) {
			db = new PrsDbContext();
			var purchaseRequest = db.PurchaseRequests.Find(prId);
			purchaseRequest.Total = purchaseRequest.PurchaseRequestLineitems
				.Sum(prli => prli.Quantity * prli.Product.Price);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				throw ex;
			}
		}

		// GET: PurchaseRequestLineitems/List
		public ActionResult List() {
			return new JsonNetResult { Data = db.PurchaseRequestLineitems.ToList() };
		}

		// GET: PurchaseRequestLineitems/Get/5
		public ActionResult Get(int? id) {
			if (id == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id cannot be null." } };
			}
			var purchaseRequestLineitem = db.PurchaseRequestLineitems.Find(id);
			if (purchaseRequestLineitem == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id not found." } };
			}
			return new JsonNetResult { Data = purchaseRequestLineitem };
		}

		// POST: PurchaseRequestLineitems/Create
		public ActionResult Create([FromBody] PurchaseRequestLineitem purchaseRequestLineitem) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			db.PurchaseRequestLineitems.Add(purchaseRequestLineitem);
			try {
				db.SaveChanges();
				CalculatePurchaseRequestTotal(purchaseRequestLineitem.PurchaseRequestId);
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Created." } };
		}

		// POST: PurchaseRequestLineitems/Change
		public ActionResult Change([FromBody] PurchaseRequestLineitem purchaseRequestLineitem) {
			if (!ModelState.IsValid || purchaseRequestLineitem.Quantity == 0) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			var purchaseRequest2 = db.PurchaseRequestLineitems.Find(purchaseRequestLineitem.Id);
			purchaseRequest2.Copy(purchaseRequestLineitem);
			try {
				db.SaveChanges();
				CalculatePurchaseRequestTotal(purchaseRequestLineitem.PurchaseRequestId);
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Changed." } };
		}

		// POST: PurchaseRequestLineitems/Remove
		public ActionResult Remove([FromBody] PurchaseRequestLineitem purchaseRequestLineitem) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			var purchaseRequest2 = db.PurchaseRequestLineitems.Find(purchaseRequestLineitem.Id);
			db.PurchaseRequestLineitems.Remove(purchaseRequest2);
			try {
				db.SaveChanges();
				CalculatePurchaseRequestTotal(purchaseRequestLineitem.PurchaseRequestId);
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Removed." } };
		}
	}
}