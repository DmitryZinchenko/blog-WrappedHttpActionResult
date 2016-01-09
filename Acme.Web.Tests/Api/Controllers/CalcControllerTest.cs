using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using Acme.Web.Api.Controllers;
using Acme.Web.Api.Helpers;
using Acme.Web.Domain;
using Acme.Web.Tests.TestHelpers;
using Moq;
using Xunit;

namespace Acme.Web.Tests.Api.Controllers
{
	public class CalcControllerTest
	{
		[Fact]
		public void CalcAValue_ReturnsOkNegotiatedContentResult()
		{
			//System.Diagnostics.Debugger.Launch();

			// arrange
			var mock = new Mock<ICalcEngine>();
			mock.Setup(x => x.Calc(It.IsInRange<int>(1, int.MaxValue, Range.Inclusive))).Returns<int>(v => 10 % v);

			var controller = new CalcController(mock.Object);

			// act
			IHttpActionResult actionResult = controller.CalcAValue(10);

			//  assert
			Assert.IsType<OkNegotiatedContentResult<int>>(actionResult);
		}

		[Fact]
		public void CalcAValue_ThrowsDivideByZeroException()
		{
			//System.Diagnostics.Debugger.Launch();

			// arrange
			var mock = new Mock<ICalcEngine>();
			mock.Setup(x => x.Calc(It.IsAny<int>())).Throws<DivideByZeroException>();

			var controller = new CalcController(mock.Object);

			// act and assert
			Assert.Throws<DivideByZeroException>(() => { controller.CalcAValue(int.MinValue); });
		}

		[Fact]
		public void CalcBValue_ReturnsOkNegotiatedContentResult()
		{
			//System.Diagnostics.Debugger.Launch();

			// arrange
			var mock = new Mock<ICalcEngine>();
			mock.Setup(x => x.Calc(It.IsInRange<int>(1, int.MaxValue, Range.Inclusive))).Returns<int>(v => 10 % v);

			var controller = new CalcController(mock.Object);

			// act
			IHttpActionResult actionResult = controller.CalcBValue(10);

			//  assert
			Assert.IsType<OkNegotiatedContentResult<int>>(actionResult);
		}

		[Fact]
		public void CalcBValue_ReturnsExceptionResult()
		{
			//System.Diagnostics.Debugger.Launch();

			// arrange
			var mock = new Mock<ICalcEngine>();
			mock.Setup(x => x.Calc(It.IsAny<int>())).Throws<DivideByZeroException>();

			var controller = new CalcController(mock.Object);

			// act
			IHttpActionResult actionResult = controller.CalcBValue(int.MinValue);

			// assert
			Assert.IsType<ExceptionResult>(actionResult);
			Assert.IsType<DivideByZeroException>(((ExceptionResult)actionResult).Exception);
		}

		[Fact]
		public void CalcCValue_ReturnsOkNegotiatedContentResult()
		{
			//System.Diagnostics.Debugger.Launch();

			// arrange
			var mock = new Mock<ICalcEngine>();
			mock.Setup(x => x.Calc(It.IsInRange<int>(1, int.MaxValue, Range.Inclusive))).Returns<int>(v => 10 % v);

			var controller = new CalcController(mock.Object);

			// act
			IHttpActionResult actionResult = controller.CalcCValue(10);

			//  assert
			Assert.IsType<OkNegotiatedContentResult<int>>(actionResult);
		}

		[Fact]
		public async void CalcCValue_ReturnsInternalServerErrorWithReasonPhrase()
		{
			//System.Diagnostics.Debugger.Launch();

			// arrange
			var mock = new Mock<ICalcEngine>();
			mock.Setup(x => x.Calc(It.IsAny<int>())).Throws<DivideByZeroException>();

			var controller = new CalcController(mock.Object).ConfigureForGet(int.MinValue);

			// act
			IHttpActionResult actionResult = controller.CalcCValue(int.MinValue);

			// assert
			Assert.IsType<WrappedHttpActionResult>(actionResult);

			var contentResult = actionResult as WrappedHttpActionResult;
			Assert.NotNull(contentResult);

			HttpResponseMessage response = await contentResult.ExecuteAsync(CancellationToken.None);
			Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
			Assert.Equal("Internal Server Error: Check your calculation!", response.ReasonPhrase);
		}
	}
}
