using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Checkout.V3
{
    public class Payment_Method
    {
        [JsonProperty(propertyName: "type")]
        public string type_message { get; set; }
        public string id { get; set; }
        public bool saved { get; set; }
        public string title { get; set; }

    }
}
