using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReceiptStatus
    {
        [EnumMember(Value = "pending")] Pending,

        [EnumMember(Value = "succeeded")] Succeeded,

        [EnumMember(Value = "canceled")] Canceled
    }
}
