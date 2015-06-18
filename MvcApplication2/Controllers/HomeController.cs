using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Security;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers
{
	public class HomeController : BaseController
	{
		private BLL.HomeServices services;
		public HomeController() { services = new BLL.HomeServices(); ViewBag.SelectedMainMenu = "Home"; }

		public ActionResult Index()
		{
			HomeModel model = services.GetHomeModel();
			return View(model);
		}

		/// <summary>
		/// add box to let user change theme color quickly (140212)
		/// </summary>
		[HttpPost]
		public ActionResult ThemeColor(string themeColor)
		{
			services.UpdateThemeColor(HttpContext.User.Identity.Name, themeColor);
			return Json(new { isError = "false" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult Logon(string logonUserName, string logonPassword, string logonRememberMe, string returnUrl)
		{
			if (Membership.ValidateUser(logonUserName, logonPassword))
			{
				bool rememberMe = false;
				if (logonRememberMe == "true") rememberMe = true;

				// to make sure the name is exactly like the UserName (ie, change 'Brian' to 'brian') (150330)
				MembershipUser membershipUser = Membership.GetUser(logonUserName);
				logonUserName = membershipUser.UserName;

				// ref: http://stackoverflow.com/questions/682788/making-user-login-persistant-with-asp-net-membership (140513)
				FormsAuthentication.SetAuthCookie(logonUserName, rememberMe);

				if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
				{
					return Redirect(returnUrl);
				}
				else
				{
					TempData["justLogonName"] = logonUserName;
					return RedirectToAction("Index", "Home");
				}
			}
			else
			{
				ViewBag.LogOnError = @Resources.Global.UserNamePasswordIncorrect;
			}

			// if we got this far, something failed, redisplay form
			HomeModel model = services.GetHomeModel();
			return View("Index", model);
		}

		[HttpPost]
		public ActionResult SignUp(string UserName, string UserEmail, string Password, string ConfirmPassword)
		{
			string msg = string.Empty;
			bool n = true;
			bool isApproved = true;

			// debug
			Membership.DeleteUser(UserName);

			// Attempt to register the user
			MembershipCreateStatus createStatus;
			Membership.CreateUser(UserName, Password, UserEmail, null, null, isApproved, null, out createStatus);

			if (createStatus == MembershipCreateStatus.Success)
			{
				// debug
				// this is the default role for all signups (130417)
				// Roles.AddUserToRole(UserName, MyRoles.UserAdmin);

				FormsAuthentication.SetAuthCookie(UserName, false);
				msg = Resources.Global.ThankYouForSigningUp1;
			}
			else
			{
				n = false;

				switch (createStatus)
				{
					case MembershipCreateStatus.DuplicateUserName:
						msg = Resources.Global.ErrorDuplicatedName;
						break;

					case MembershipCreateStatus.DuplicateEmail:
						msg = Resources.Global.ErrorEmailAlreadyUsed;
						break;

					default:
						msg = Resources.Global.ErrorUnknown;
						break;
				}
			}

			return Json(new { resultMsg = msg, result = n }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult SqlServerTest(string sqlConnectionString, string sqlCommand)
		{
			string result = services.SqlServerTest(sqlConnectionString, sqlCommand);

			return Json(new { result = result }, JsonRequestBehavior.AllowGet);
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
