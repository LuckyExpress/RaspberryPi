using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace MvcApplication2.Controllers
{
	public class HomeController : BaseController
	{
		private BLL.HomeServices services;
		public HomeController() { services = new BLL.HomeServices(); ViewBag.SelectedMainMenu = "Home"; }

		public ActionResult Index()
		{
			ViewBag.Message = "Welcome to ASP.NET MVC!";
			return View();
		}

		public ActionResult About()
		{
			string sql = "SELECT COUNT(*) FROM igpsite.Employee";

			string connectionString = "Server=stresstest.iguardpayroll.com;Database=LuckyTechnology;User ID=lucky;Password=HappyNewYear2015;TrustServerCertificate=False;Trusted_Connection=False;Encrypt=False;MultipleActiveResultSets=True;Max Pool Size=1024;Connection Timeout=60;TrustServerCertificate=True";
			connectionString = "Data Source=stresstest.iguardpayroll.com;Initial Catalog=LuckyTechnology;Persist Security Info=True;User ID=lucky;Password=HappyNewYear2015;Encrypt=False";

			var builder = new SqlConnectionStringBuilder();
			builder.DataSource = "stresstest.iguardpayroll.com";
			builder.Encrypt = false;
			builder.TrustServerCertificate = false;
			builder.InitialCatalog = "LuckyTechnology";
			builder.PersistSecurityInfo = true;
			builder.UserID = "lucky";
			builder.Password = "HappyNewYear2015";
			builder.ToString();

			using (SqlConnection sn = new SqlConnection())
			{
				sn.ConnectionString = connectionString;
				sn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = sql;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.Connection = sn;

				int n = (int)cmd.ExecuteScalar();

				ViewBag.Message = n.ToString();
				ViewBag.ConnectionString = builder.ToString();
			}

			return View();
		}
	}
}
