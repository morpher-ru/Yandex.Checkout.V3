using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DealStatus
    {
        [EnumMember(Value = "opened")]
        Opened,
        [EnumMember(Value = "closed")]
        Closed,
    }
}
