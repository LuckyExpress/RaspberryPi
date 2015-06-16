using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.BLL
{
	public class BaseServices : MyBase
	{
		public LayoutModel GetLayoutModel(string selectedMainMenu)
		{
			LayoutModel n = new LayoutModel();
			dynamic profile;

			if (HttpContext.Current.Request.IsAuthenticated == true)
			{
				n.greetingName = HttpContext.Current.User.Identity.Name;

				// profile could have been saved in GetHomeModel() (140213)
				profile = System.Web.HttpContext.Current.Items["currentProfile"];
				if (profile == null) profile = System.Web.Profile.ProfileBase.Create(HttpContext.Current.User.Identity.Name);

				n.theme = profile.Preferences.Theme;
				if (string.IsNullOrEmpty(n.theme) == true) n.theme = "Aquamarine";
			}
			else
			{
				n.theme = "Black";
			}

			n.controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();

			return n;
		}
	}
}