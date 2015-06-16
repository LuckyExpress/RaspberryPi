using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class BaseController : Controller
    {
		private BLL.BaseServices baseServices = new BLL.BaseServices();

		public BaseController()
		{
			ViewBag.GetLayoutModel = new Func<LayoutModel>(GetLayoutModel);
		}

		public LayoutModel GetLayoutModel()
		{
			return baseServices.GetLayoutModel(ViewBag.SelectedMainMenu);
		}

		/// <summary>
		/// called when user select language (120620)
		/// </summary>
		public void culture(string c)
		{
			HttpCookie cookie = new HttpCookie("PreferedCulture");
			cookie.Expires = DateTime.Now.AddDays(365);

			cookie.Value = c;
			Response.Cookies.Add(cookie);

			System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);

			if (Request.UrlReferrer != null)
			{
				// to get rid of the possible action name in the url (120808)
				Uri uri = new Uri(Request.UrlReferrer.AbsoluteUri);
				int segmentCount = uri.Segments.Count();
				string url = uri.Scheme + Uri.SchemeDelimiter + uri.Authority + uri.Segments[0];
				if (segmentCount >= 2) url += uri.Segments[1];

				// get querystring if existed 21/11/2014 (did stanley add this?) (141121)
				string parm = uri.Query;
				if (parm != string.Empty) url += parm;

				Response.Redirect(url);
			}
			else
			{
				Response.Redirect("/");
			}
		}
    }
}
