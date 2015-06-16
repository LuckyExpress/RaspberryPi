using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.BLL
{
	public class BaseServices
	{
		public LayoutModel GetLayoutModel()
		{
			LayoutModel model = new LayoutModel();

			if (HttpContext.Current.Request.IsAuthenticated == true)
			{
				model.greetingName = HttpContext.Current.User.Identity.Name;
			}

			model.controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();

			return model;
		}
	}
}