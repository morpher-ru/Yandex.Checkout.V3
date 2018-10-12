using System.Runtime.Serialization;

namespace Yandex.Checkout.V3
{
    public enum Event
    {
        [EnumMember(Value = "payment.waiting_for_capture")]
        PaymentWaitingForCapture = 1,
        [EnumMember(Value = "payment.succeeded")]
        Succeeded = 2
    }
}
