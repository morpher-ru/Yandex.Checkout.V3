using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    public class Confirmation
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ConfirmationType Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ReturnUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ConfirmationUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enforce { get; set; }

        public string Locale { get; set; }
    }
}
