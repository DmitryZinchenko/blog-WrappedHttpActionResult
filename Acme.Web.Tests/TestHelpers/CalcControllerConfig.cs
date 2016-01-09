using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Acme.Web.Api.Controllers;

namespace Acme.Web.Tests.TestHelpers
{
	public static class CalcControllerConfig
	{
		public static CalcController ConfigureForGet(this CalcController controller, int value)
		{
			var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, string.Format("http://localhost/api/calc/c/{0}", value));
            IHttpRoute route = config.Routes.MapHttpRoute(name: "CalcApi", routeTemplate: "api/calc/c/{value}");
			var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "value", value } });
			controller.ControllerContext = new HttpControllerContext(config, routeData, request);
			controller.Request = request;
			controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

			return controller;
		}
	}
}
