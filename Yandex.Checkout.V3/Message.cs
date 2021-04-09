using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    public class Message
    {
        public string Type { get; init; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Event Event { get; init; }

        public Payment Object { get; init; }
    }
}
