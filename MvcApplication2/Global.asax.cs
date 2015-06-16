using System;
using System.Collections.Generic;
using System.Data.Entity;
// using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication2
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	// ref http://mono.1490590.n4.nabble.com/MVC-3-App-Cannot-Find-Template-td3353175.html && https://gist.github.com/carlhoerberg/870574 (150608)
	public class MonoRazorViewEngine : RazorViewEngine
	{
		protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
		{
			return true;
			// return base.FileExists(controllerContext, virtualPath.Replace("~", ""));
		}
	}

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
			// ref http://mono.1490590.n4.nabble.com/MVC-3-App-Cannot-Find-Template-td3353175.html && https://gist.github.com/carlhoerberg/870574 (150608)
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new MonoRazorViewEngine());

			AreaRegistration.RegisterAllAreas();

			// Use LocalDB for Entity Framework by default
			// Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}