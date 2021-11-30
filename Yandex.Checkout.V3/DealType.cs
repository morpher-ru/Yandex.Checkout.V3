using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DealType
    {
        [EnumMember(Value = "safe_deal")]
        SafeDeal
    }
}
