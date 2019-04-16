namespace Yandex.Checkout.V3
{
    public class Message
    {
        public string Type { get; set; }

        //[JsonConverter(typeof(EventConverter))]
        public Event Event { get; set; }

        public Payment Object { get; set; }
    }
}
