using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FeeMomentType
    {
        [EnumMember(Value = "deal_closed")]
        DealClosed,
        [EnumMember(Value = "payment_succeeded")]
        PaymentSucceeded
    }
}
