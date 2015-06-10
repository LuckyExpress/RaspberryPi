using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_01
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MonoWebFormViewEngine : WebFormViewEngine
	{
		protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
		{
			return base.FileExists(controllerContext, virtualPath.Replace("~", ""));
		}
	}

	public class MonoRazorViewEngine : RazorViewEngine
	{
		protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
		{
			return base.FileExists(controllerContext, virtualPath.Replace("~", ""));
		}
	}

	//public class MvcApplication : System.Web.HttpApplication
	//{
	//	protected void Application_Start()
	//	{
	//		ViewEngines.Engines.Clear();
	//		ViewEngines.Engines.Add(new MonoWebFormViewEngine());
	//		ViewEngines.Engines.Add(new MonoRazorViewEngine());
	//		// The normal stuff..
	//	}
	//}

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new MonoWebFormViewEngine());
			ViewEngines.Engines.Add(new MonoRazorViewEngine());

			AreaRegistration.RegisterAllAreas();
			
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}