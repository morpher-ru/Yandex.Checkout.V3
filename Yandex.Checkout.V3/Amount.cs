using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Payment amount
    /// </summary>
    public class Amount
    {
        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty()]
        public decimal Value { get; set; }

        /// <summary>
        /// Three letter currency code (ex: RUB)
        /// </summary>
        public string Currency { get; set; } = "RUB";
    }
}
