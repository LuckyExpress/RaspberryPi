using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.Models
{
	public class HomeModel
	{
		public string userName { get; set; }
		public List<string> themeList { get; set; }
		public string currentTheme { get; set; }
		public string sqlServerConnectionString { get; set; }
		public string sqlCommand { get; set; }
	}
}