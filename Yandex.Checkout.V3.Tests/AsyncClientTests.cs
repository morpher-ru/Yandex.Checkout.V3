using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Checkout.V3.Tests
{
    [TestClass]
    public class AsyncClientTests
    {
        [TestMethod]
        public async Task UnauthorizedThrowsException()
        {
            async Task Action() => await SendAsync(
                async ac => await ac.GetPaymentAsync("paymentId"), 
                new HttpResponseMessage(HttpStatusCode.Unauthorized));

            await Assert.ThrowsExceptionAsync<YandexCheckoutException>(Action);
        }

        static async Task<HttpResponseMessage> SendAsync(Func<AsyncClient, Task> action, HttpResponseMessage httpResponseMessage)
        {
            var messageHandler = new TestMessageHandler();
            messageHandler.ResponseQueue.Enqueue(httpResponseMessage);

            var httpClient = new HttpClient(messageHandler) {BaseAddress = new Uri("http://ym.com")};

            var asyncClient = new AsyncClient(httpClient);
            await action(asyncClient);
            return messageHandler.ResponseQueue.Dequeue();
        }
    }
}
