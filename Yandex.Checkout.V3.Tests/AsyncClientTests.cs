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
        // ReSharper disable StringLiteralTypo
        private readonly Client _clientInvalidPasswordFormat = 
            new("fake shop id", "fake key");
        private readonly Client _clientInvalidLoginFormat = 
            new("fake shop id", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI");
        private readonly Client _clientIncorrectLoginOrPassword = 
            new("501156", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRAAA");
        // ReSharper restore StringLiteralTypo

        private readonly NewPayment _newPayment = new()
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
            Assert.AreEqual("Incorrect password format in the Authorization header. Use Secret key issued in Merchant Profile as the password", ex.Error.Description);
        }

        [TestMethod]
        public async Task UnauthorizedInvalidKeyFormatThrowsException()
        {
            var asyncClient = _clientInvalidLoginFormat.MakeAsync();
            async Task Action() => await asyncClient.CreatePaymentAsync(_newPayment);
            YandexCheckoutException ex = await Assert.ThrowsExceptionAsync<YandexCheckoutException>(Action);
            Assert.AreEqual(HttpStatusCode.Unauthorized, ex.StatusCode);
            Assert.AreEqual("Login has illegal format", ex.Error.Description);
        }

        [TestMethod]
        public async Task UnauthorizedIncorrectLoginOrPasswordThrowsException()
        {
            var asyncClient = _clientIncorrectLoginOrPassword.MakeAsync();
            async Task Action() => await asyncClient.CreatePaymentAsync(_newPayment);
            YandexCheckoutException ex = await Assert.ThrowsExceptionAsync<YandexCheckoutException>(Action);
            Assert.AreEqual(HttpStatusCode.Unauthorized, ex.StatusCode);
            Assert.AreEqual("Error in shopId or secret key. Check their validity. You can reissue the key in the Merchant Profile", ex.Error.Description);
        }

        [TestMethod]
        public async Task UnauthorizedThrowsException()
        {
            async Task Action() => await SendAsync(
                async ac => await ac.GetPaymentAsync("paymentId"),
                new HttpResponseMessage(HttpStatusCode.Unauthorized));

            await Assert.ThrowsExceptionAsync<YandexCheckoutException>(Action);
        }

        private static async Task SendAsync(Func<AsyncClient, Task> action, HttpResponseMessage httpResponseMessage)
        {
            var messageHandler = new TestMessageHandler();
            messageHandler.ResponseQueue.Enqueue(httpResponseMessage);

            var httpClient = new HttpClient(messageHandler);

            var client = new Client("test_shop_id", "test_key", "http://ym.com");
            
            var asyncClient = new AsyncClient(httpClient, false, client);
            
            await action(asyncClient);
        }
    }
}
