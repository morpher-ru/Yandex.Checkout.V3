using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    public class Confirmation
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ConfirmationType type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string return_url { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string confirmation_url { get; set; }
    }

    public enum ConfirmationType
    {
        redirect,
        external
    }
}
