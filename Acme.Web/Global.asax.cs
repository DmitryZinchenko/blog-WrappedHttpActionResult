﻿using System;
using System.Web.Http;

namespace Acme.Web
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			GlobalConfiguration.Configure(FormatterConfig.Register);
			GlobalConfiguration.Configure(AutofacConfig.Register);
		}
	}
}