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
    public class UsersController : Controller
    {
		private PrsDbContext db = new PrsDbContext();

		// GET: Users/Login/username/password
		public ActionResult Login(string username, string password) {
			if(username == null || password == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Invalid username/password." } };
			}
			var user = db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
			if (user == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Invalid username/password." } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Login successful.", Data = user } };
		}

		// GET: Users/List
		public ActionResult List() {
			return new JsonNetResult { Data = db.Users.ToList() };
        }

		// GET: Users/Get/5
		public ActionResult Get(int? id) {
			if (id == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id cannot be null." } }; 
			}
			var user = db.Users.Find(id);
			if(user == null) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Id not found." } };
			}
			return new JsonNetResult { Data = user };
		}

		// POST: Users/Create
		public ActionResult Create([FromBody] User user) {
			user.DateCreated = DateTime.Now;
			if(!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			db.Users.Add(user);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Created." } };
		}

		// POST: Users/Change
		public ActionResult Change([FromBody] User user) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			var user2 = db.Users.Find(user.Id);
			user2.Copy(user);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Changed." } };
		}

		// POST: Users/Remove
		public ActionResult Remove([FromBody] User user) {
			if (!ModelState.IsValid) {
				var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
			}
			var user2 = db.Users.Find(user.Id);
			db.Users.Remove(user2);
			try {
				db.SaveChanges();
			} catch (Exception ex) {
				return new JsonNetResult { Data = new Msg { Result = "Failed", Message = ex.Message } };
			}
			return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Removed." } };
		}

	}
}