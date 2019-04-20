using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Yandex.Checkout.V3.Tests
{
    public class TestMessageHandler : HttpMessageHandler
    {
        public Queue<HttpRequestMessage> RequestQueue { get; } = new Queue<HttpRequestMessage>();
        public Queue<HttpResponseMessage> ResponseQueue { get; } = new Queue<HttpResponseMessage>();

        protected sealed override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestQueue.Enqueue(request);
            return Task.FromResult(ResponseQueue.Dequeue());
        }
    }
}