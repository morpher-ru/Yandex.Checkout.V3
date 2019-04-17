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
    }
}
