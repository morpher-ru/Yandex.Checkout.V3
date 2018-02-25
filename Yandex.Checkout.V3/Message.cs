using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
    public class Message
    {
        public string type { get; set; }

        [JsonConverter(typeof(EventConverter))]
        public Event @event { get; set; }

        public Payment @object { get; set; }
    }
}
