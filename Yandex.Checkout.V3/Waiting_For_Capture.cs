using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Checkout.V3
{
    public class Waiting_For_Capture
    {
        [JsonProperty(propertyName: "type")]
        public string  type_messahe { get; set; }
        [JsonProperty(propertyName: "event")]
        public string event_status { get; set; }
        [JsonProperty(propertyName: "object")]
        public PayDetail object_pay { get; set; }
    }
}
