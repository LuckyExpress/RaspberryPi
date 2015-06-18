using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

			// for testing sqlServer connection (150618)
			model.sqlServerConnectionString = "Data Source=stresstest.iguardpayroll.com;Initial Catalog=LuckyTechnology;Persist Security Info=True;User ID=lucky;Password=HappyNewYear2015;Encrypt=False";
			model.sqlCommand = "SELECT COUNT(*) FROM igpsite.Employee";

			return model;
		}

		public void UpdateThemeColor(string userName, string theme)
		{
			UpdateTheme(userName, theme);
			return;
		}

		public string SqlServerTest(string sqlConnectionString, string sqlCommand)
		{
			string n = string.Empty;
			int result;

			/*
			var builder = new SqlConnectionStringBuilder();
			builder.DataSource = "stresstest.iguardpayroll.com";
			builder.Encrypt = false;
			builder.TrustServerCertificate = false;
			builder.InitialCatalog = "LuckyTechnology";
			builder.PersistSecurityInfo = true;
			builder.UserID = "lucky";
			builder.Password = "HappyNewYear2015";
			builder.ToString();
			*/

			using (SqlConnection sn = new SqlConnection())
			{
				sn.ConnectionString = sqlConnectionString;
				sn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = sqlCommand;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.Connection = sn;

				try
				{
					result = (int)cmd.ExecuteScalar();
					n = result.ToString();
				}
				catch (Exception e)
				{
					n = e.Message;
				}
			}

			return n;
		}
	}
}