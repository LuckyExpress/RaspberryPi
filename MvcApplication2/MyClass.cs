using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2
{
	public class MyRoles
	{
		public static string Administrators = "Administrators";
		public static string UserAdmin = "User Admin";
		public static string CoAdmin = "Co-Admin";
		public static string GeneralUser = "General User";
		public static string NotMember = "Not Member";
		public static string Editor = "Editor";
		public static string Internal = "Internal";						// mainly for showing the "ManageSite" & "ManageiGuardExpress" tabs in Dealer page (comment (150224)
		public static string RealTimeMonitor = "RealTimeMonitor";
		public static string CoViewer = "Co-Viewer";
		public static string Dealer = "Dealer";
	}
}