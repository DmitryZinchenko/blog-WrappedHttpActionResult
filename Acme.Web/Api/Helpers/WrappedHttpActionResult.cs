using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Acme.Web.Api.Helpers
{
    // https://gist.github.com/sixeyed/10118609
    public class WrappedHttpActionResult : IHttpActionResult
    {
        private readonly IHttpActionResult _innerResult;
        private readonly string _responsePhrase;
        private readonly Action<HttpResponseMessage> _responseAction;

        public WrappedHttpActionResult(IHttpActionResult inner, Action<HttpResponseMessage> responseAction)
            : this(inner, null, responseAction)
        {
        }

        public WrappedHttpActionResult(IHttpActionResult inner, string responsePhrase, Action<HttpResponseMessage> responseAction = null)
        {
            _innerResult = inner;
            _responsePhrase = responsePhrase;
            _responseAction = responseAction;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _innerResult.ExecuteAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(_responsePhrase))
            {
                response.ReasonPhrase = _responsePhrase;
            }

            if (_responseAction != null)
            {
                _responseAction(response);
            }

            return response;
        }
    }
}