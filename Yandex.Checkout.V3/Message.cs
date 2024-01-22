using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Yandex.Checkout.V3
{
    public class Message : RawJsonBase
    {
        public string Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Event Event { get; set; }

        public Payment Payment { get; set; }

        public Refund Refund { get; set; }
    }

    internal class MessageInternal<T>
    {
        public string Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Event Event { get; set; }

        public T Object { get; set; }
    }

    public class RawJsonBase
    {
        public string RawJson { get; set; }
    }
}
