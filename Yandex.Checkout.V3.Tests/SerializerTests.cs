using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Checkout.V3.Tests
{
    [TestClass]
    public class SerializerTests
    {
        [TestMethod]
        public void EnumEventSerializedCorrectly()
        {
            string s = Serializer.SerializeObject(new Message{Event = Event.PaymentWaitingForCapture});
            Assert.AreEqual("{\"event\":\"payment.waiting_for_capture\"}", s);
        }

        [TestMethod]
        public void RefundReceiptRegistrationDeserializedCorrectly()
        {
            var refund = Serializer.DeserializeObject<Refund>("{\"receipt_registration\":\"succeeded\", \"amount\":{\"value\":1}, \"payment_id\":1}");
            Assert.AreEqual(ReceiptRegistrationStatus.Succeeded, refund.ReceiptRegistration);
        }

        [TestMethod]
        public void RefundReceiptRegistrationSucceededSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new Refund {ReceiptRegistration = ReceiptRegistrationStatus.Succeeded });
            Assert.AreEqual("{\"status\":\"succeeded\",\"created_at\":\"0001-01-01T00:00:00\",\"receipt_registration\":\"succeeded\"}", s);
        }

        [TestMethod]
        public void RefundReceiptRegistrationNullSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new Refund());
            Assert.AreEqual("{\"status\":\"succeeded\",\"created_at\":\"0001-01-01T00:00:00\"}", s);
        }

        [TestMethod]
        public void CreatePayoutRequestSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new NewPayout {PayoutToken = "token"});
            Assert.AreEqual("{\"payout_token\":\"token\"}", s);
        }

        [TestMethod]
        public void CardPaymentMethodDeserializedCorrectly()
        {
            var card = Serializer.DeserializeObject<Card>("{" +
                                                          "\"first6\": \"555555\"," +
                                                          "\"last4\": \"4444\"," +
                                                          "\"expiry_month\": \"07\"," +
                                                          "\"expiry_year\": \"2022\"," +
                                                          "\"card_type\": \"MasterCard\"," +
                                                          "\"issuer_country\": \"RU\"," +
                                                          "\"issuer_name\": \"Sberbank\"}");
            Assert.AreEqual("555555", card.First6);
            Assert.AreEqual("4444", card.Last4);
            Assert.AreEqual("07", card.ExpiryMonth);
            Assert.AreEqual("2022", card.ExpiryYear);
            Assert.AreEqual("MasterCard", card.CardType);
            Assert.AreEqual("RU", card.IssuerCountry);
            Assert.AreEqual("Sberbank", card.IssuerName);
        }
    }
}
