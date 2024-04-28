using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Yandex.Checkout.V3.Tests
{
    public class TestMessageHandler : HttpMessageHandler
    {
        public Queue<HttpResponseMessage> ResponseQueue { get; } = new();

        protected sealed override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(ResponseQueue.Dequeue());
        }
    }
}
