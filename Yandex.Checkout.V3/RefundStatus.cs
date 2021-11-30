using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RefundStatus
    {
        [EnumMember(Value = "succeeded")]
        Succeeded,
        [EnumMember(Value = "canceled")]
        Canceled
    }
}
