using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.BLL
{
	public class HomeServices : BaseServices
	{
		public HomeModel GetHomeModel()
		{
			HomeModel model = new HomeModel();
			model.userName = null;

			if (HttpContext.Current.Request.IsAuthenticated == true)
			{
				model.userName = HttpContext.Current.User.Identity.Name;

				dynamic profile = System.Web.Profile.ProfileBase.Create(HttpContext.Current.User.Identity.Name);
				model.currentTheme = profile.Preferences.Theme;

				// save it for later use (such as in GetLayoutModel()) (140213)
				System.Web.HttpContext.Current.Items["currentProfile"] = profile;
			}

			// for the new themeColor box (140212)
			model.themeList = GetThemeList();

			return model;
		}

		public void UpdateThemeColor(string userName, string theme)
		{
			UpdateTheme(userName, theme);
			return;
		}
	}
}