using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2
{
	public class MyBase
	{
		protected bool isEnableCaching = true;

		/// <summary>
		/// shared by the theme box in home page & _MamberDetail (150414)
		/// </summary>
		public List<string> GetThemeList()
		{
			List<string> n = null;

			string key = "ThemeList";

			if ((isEnableCaching == false) || ((n = (List<string>)HttpRuntime.Cache[key]) == null))
			{
				n = new List<string>();

				string themesDirPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/themes/MyThemes");
				string[] themeColors = System.IO.Directory.GetDirectories(themesDirPath);

				for (int i = 0; i < themeColors.Length; i++) n.Add(System.IO.Path.GetFileName(themeColors[i]));

				// remove black (120209)
				n.RemoveAt(n.IndexOf("Black"));

				// cache the array with a dependency to the folder
				System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(themesDirPath);
				HttpRuntime.Cache.Insert(key, n, dep);
			}

			return n;
		}

		public void UpdateTheme(string userName, string theme)
		{
			dynamic profile = System.Web.Profile.ProfileBase.Create(userName);
			profile.Preferences.Theme = theme;
			profile.Save();

			return;
		}
	}
}