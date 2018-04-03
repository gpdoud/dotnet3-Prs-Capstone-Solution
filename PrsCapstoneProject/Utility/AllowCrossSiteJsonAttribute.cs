using System;
using System.Web.Mvc;

namespace PrsCapstoneProject.Utility {

	public class AllowCrossSiteJsonAttribute : ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			//filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
			//base.OnActionExecuting(filterContext);
			// We'd normally just use "*" for the allow-origin header, 
			// but Chrome (and perhaps others) won't allow you to use authentication if
			// the header is set to "*".
			// TODO: Check elsewhere to see if the origin is actually on the list of trusted domains.
			var ctx = filterContext.RequestContext.HttpContext;
			var origin = ctx.Request.Headers["Origin"];
			var allowOrigin = !string.IsNullOrWhiteSpace(origin) ? origin : "*";
			ctx.Response.AddHeader("Access-Control-Allow-Origin", allowOrigin);
			ctx.Response.AddHeader("Access-Control-Allow-Headers", "*");
			ctx.Response.AddHeader("Access-Control-Allow-Credentials", "true");
			base.OnActionExecuting(filterContext);
		}
	}
}