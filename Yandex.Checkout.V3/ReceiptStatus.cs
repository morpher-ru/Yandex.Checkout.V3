using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Yandex.Checkout.V3;

[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum ReceiptStatus
{
    Pending,

    Succeeded,

    Canceled
}