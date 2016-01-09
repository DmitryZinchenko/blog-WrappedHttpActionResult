using System;
using System.Net.Http;
using System.Web.Http;

namespace Acme.Web.Api.Helpers
{
	// https://gist.github.com/sixeyed/10118609
	public static class IHttpActionResultExtensions
	{
		public static IHttpActionResult With(this IHttpActionResult inner, string responsePhrase = null, Action<HttpResponseMessage> responseAction = null)
		{
			return new WrappedHttpActionResult(inner, responsePhrase, responseAction);
		}

		public static IHttpActionResult With(this IHttpActionResult inner, Action<HttpResponseMessage> responseAction)
		{
			return new WrappedHttpActionResult(inner, responseAction);
		}
	}
}