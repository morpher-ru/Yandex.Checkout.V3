using System.Runtime.Serialization;

namespace Yandex.Checkout.V3
{
    public enum PaymentStatus
    {
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "waiting_for_capture")]
        WaitingForCapture,
        [EnumMember(Value = "succeeded")]
        Succeeded,
        [EnumMember(Value = "canceled")]
        Canceled
    }
}
