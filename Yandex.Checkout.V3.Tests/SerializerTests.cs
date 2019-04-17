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
            Assert.AreEqual("{\"status\":\"pending\",\"created_at\":\"0001-01-01T00:00:00\",\"receipt_registration\":\"succeeded\"}", s);
        }

        [TestMethod]
        public void RefundReceiptRegistrationNullSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new Refund {});
            Assert.AreEqual("{\"status\":\"pending\",\"created_at\":\"0001-01-01T00:00:00\"}", s);
        }
    }
}
