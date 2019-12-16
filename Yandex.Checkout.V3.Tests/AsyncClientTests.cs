using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yandex.Checkout.V3.Tests
{
    [TestClass]
    public class AsyncClientTests
    {
        static readonly Client _clientInvalidPasswordFormat = new Client("fake shop id", "fake key");
        static readonly Client _clientInvalidLoginFormat = new Client("fake shop id", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI");
        static readonly Client _clientIncorrectLoginOrPassword = new Client("501156", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRAAA");

        static readonly NewPayment _newPayment = new NewPayment
        {
            Amount = new Amount { Value = 10, Currency = "RUB" },
            Confirmation = new Confirmation { Type = ConfirmationType.Redirect }
        };

        [TestMethod]
        public async Task UnauthorizedInvalidPasswordFormatThrowsException()
        {
            var asyncClient = _clientInvalidPasswordFormat.MakeAsync();
            async Task Action() => await asyncClient.CreatePaymentAsync(_newPayment);
            YandexCheckoutException ex = await Assert.ThrowsExceptionAsync<YandexCheckoutException>(Action);
            Assert.AreEqual(HttpStatusCode.Unauthorized, ex.StatusCode);
            Assert.AreEqual("Incorrect password format in the Authorization header. Use Secret key issued in Merchant Profile as the password", ex.Message);
        }

        [TestMethod]
        public async Task UnauthorizedInvalidKeyFormatThrowsException()
        {
            var asyncClient = _clientInvalidLoginFormat.MakeAsync();
            async Task Action() => await asyncClient.CreatePaymentAsync(_newPayment);
            YandexCheckoutException ex = await Assert.ThrowsExceptionAsync<YandexCheckoutException>(Action);
            Assert.AreEqual(HttpStatusCode.Unauthorized, ex.StatusCode);
            Assert.AreEqual("Login has illegal format", ex.Message);
        }

        [TestMethod]
        public async Task UnauthorizedIncorrectLoginOrPasswordThrowsException()
        {
            var asyncClient = _clientIncorrectLoginOrPassword.MakeAsync();
            async Task Action() => await asyncClient.CreatePaymentAsync(_newPayment);
            YandexCheckoutException ex = await Assert.ThrowsExceptionAsync<YandexCheckoutException>(Action);
            Assert.AreEqual(HttpStatusCode.Unauthorized, ex.StatusCode);
            Assert.AreEqual("Error in shopId or secret key. Check their validity. You can reissue the key in the Merchant Profile", ex.Message);
        }

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
