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
    public class PurchaseRequestsController : Controller
    {
		private PrsDbContext db = new PrsDbContext();

		// GET: PurchaseRequests/ReviewList
		public ActionResult ReviewList() {
			return new JsonNetResult { Data = db.PurchaseRequests.Where(pr => pr.Status.Equals("REVIEW")).ToList() };
		}

		// GET: PurchaseRequests/List
		public ActionResult List() {
			return new JsonNetResult { Data = db.PurchaseRequests.ToList() };
		}

		// GET: PurchaseRequests/Get/5
		public ActionResult Get(int? id) {
			if (id == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id cannot be null." } };
			}
			var purchaseRequest = db.PurchaseRequests.Find(id);
			if (purchaseRequest == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id not found." } };
			}
			return new JsonNetResult { Data = purchaseRequest };
		}

		// POST: PurchaseRequests/Create
		public ActionResult Create([FromBody] PurchaseRequest purchaseRequest) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			db.PurchaseRequests.Add(purchaseRequest);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Created." } };
		}

		// POST: PurchaseRequests/Change
		public ActionResult Change([FromBody] PurchaseRequest purchaseRequest) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			var purchaseRequest2 = db.PurchaseRequests.Find(purchaseRequest.Id);
			purchaseRequest2.Copy(purchaseRequest);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Changed." } };
		}

		// POST: PurchaseRequests/Remove
		public ActionResult Remove([FromBody] PurchaseRequest purchaseRequest) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			var purchaseRequest2 = db.PurchaseRequests.Find(purchaseRequest.Id);
			db.PurchaseRequests.Remove(purchaseRequest2);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Removed." } };
		}
	}
}