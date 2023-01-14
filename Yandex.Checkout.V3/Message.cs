using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Yandex.Checkout.V3
{
    public class Message
    {
        public string Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Event Event { get; set; }

        public Payment Object { get; set; }
    }
}
