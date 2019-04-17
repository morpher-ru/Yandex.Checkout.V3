using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VatDataType
    {
        [EnumMember(Value = "calculated")] Calculated,
        [EnumMember(Value = "untaxed")] Untaxed
    }
}
