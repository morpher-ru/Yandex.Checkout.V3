using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Event
    {
        [EnumMember(Value = "payment.waiting_for_capture")]
        PaymentWaitingForCapture = 1,
        [EnumMember(Value = "payment.succeeded")]
        PaymentSucceeded,
        [EnumMember(Value = "payment.canceled")]
        PaymentCanceled,
        [EnumMember(Value = "refund.succeeded")]
        RefundSucceeded,
    }
}
