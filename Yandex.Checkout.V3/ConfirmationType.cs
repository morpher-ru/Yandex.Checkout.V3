using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConfirmationType
    {
        [EnumMember(Value = "redirect")]
        Redirect,
        [EnumMember(Value = "external")]
        External,
        [EnumMember(Value = "embedded")]
        Embedded,
        [EnumMember(Value = "qr")]
        QR
    }
}
