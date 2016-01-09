using System.Reflection;
using System.Web.Http;
using Acme.Web.Domain;
using Autofac;
using Autofac.Integration.WebApi;

namespace Acme.Web
{
	public static class AutofacConfig
	{
		public static void Register(HttpConfiguration configuration)
		{
			var builder = new ContainerBuilder();

			// register your Web API controllers
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			// register custom types
			builder.RegisterType<CalcEngine>().As<ICalcEngine>();

			// set the dependency resolver to be Autofac
			var container = builder.Build();
			configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}
	}
}