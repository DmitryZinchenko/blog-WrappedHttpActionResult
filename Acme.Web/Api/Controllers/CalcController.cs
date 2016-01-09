using System;
using System.Web.Http;
using Acme.Web.Api.Helpers;
using Acme.Web.Domain;

namespace Acme.Web.Api.Controllers
{
	public class CalcController : ApiController
	{
		private readonly ICalcEngine _calcEngine;

		public CalcController(ICalcEngine calcEngine)
		{
			_calcEngine = calcEngine;
		}

		[HttpGet, Route("api/calc/a/{value}")]
		public IHttpActionResult CalcAValue(int value)
		{
			return Ok(_calcEngine.Calc(value));
		}

		[HttpGet, Route("api/calc/b/{value}")]
		public IHttpActionResult CalcBValue(int value)
		{
			try
			{
				return Ok(_calcEngine.Calc(value));
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		[HttpGet, Route("api/calc/c/{value}")]
		public IHttpActionResult CalcCValue(int value)
		{
			try
			{
				return Ok(_calcEngine.Calc(value));
			}
			catch (Exception ex)
			{
				return InternalServerError(ex).With("Internal Server Error: Check your calculation!");
			}
		}
	}
}
