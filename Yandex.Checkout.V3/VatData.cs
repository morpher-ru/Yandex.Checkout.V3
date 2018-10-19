using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
    public class VatData
    {
        public VatDataType Type { get; set; }
        public string Rate { get; set; }
        public Amount Amount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PayerBankDetails PayerBankDetails { get; set; }
    }
}
