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
        public decimal Value { get; init; }

        /// <summary>
        /// Three letter currency code (ex: RUB)
        /// </summary>
        public string Currency { get; init; } = "RUB";
    }
}
