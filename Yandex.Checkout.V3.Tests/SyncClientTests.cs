using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace Yandex.Checkout.V3.Tests
{
    [TestClass]
    public class SyncClientTests
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
        public void UnauthorizedInvalidPasswordFormatThrowsException()
        {
            object Action() => _clientInvalidPasswordFormat.CreatePayment(_newPayment);
            YandexCheckoutException ex = Assert.ThrowsException<YandexCheckoutException>(Action);
            Assert.AreEqual(HttpStatusCode.Unauthorized, ex.StatusCode);
            Assert.AreEqual("Incorrect password format in the Authorization header. Use Secret key issued in Merchant Profile as the password", ex.Message);
        }

        [TestMethod]
        public void UnauthorizedInvalidKeyFormatThrowsException()
        {
            object Action() => _clientInvalidLoginFormat.CreatePayment(_newPayment);
            YandexCheckoutException ex = Assert.ThrowsException<YandexCheckoutException>(Action);
            Assert.AreEqual(HttpStatusCode.Unauthorized, ex.StatusCode);
            Assert.AreEqual("Login has illegal format", ex.Message);
        }

        [TestMethod]
        public void UnauthorizedIncorrectLoginOrPasswordThrowsException()
        {
            object Action() => _clientIncorrectLoginOrPassword.CreatePayment(_newPayment);
            YandexCheckoutException ex = Assert.ThrowsException<YandexCheckoutException>(Action);
            Assert.AreEqual(HttpStatusCode.Unauthorized, ex.StatusCode);
            Assert.AreEqual("Error in shopId or secret key. Check their validity. You can reissue the key in the Merchant Profile", ex.Message);
        }
    }
}
