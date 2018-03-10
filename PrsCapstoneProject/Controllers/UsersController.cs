using PrsCapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrsCapstoneProject.Controllers
{
    public class UsersController : Controller
    {
		private PrsDbContext db = new PrsDbContext();

        // GET: Users/List
        public ActionResult List() {
            return View();
        }
    }
}