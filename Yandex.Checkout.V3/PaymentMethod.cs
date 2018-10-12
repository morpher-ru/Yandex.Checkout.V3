using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    public class PaymentMethod
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentMethodType Type { get; set; }

        public string Id { get; set; }
        public bool Saved { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
    }
}
