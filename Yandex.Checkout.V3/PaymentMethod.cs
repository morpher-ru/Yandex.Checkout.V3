using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    public class PaymentMethod
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentMethodType type { get; set; }

        public Guid id { get; set; }
        public bool saved { get; set; }
        public string title { get; set; }
        public string phone { get; set; }
    }
}
