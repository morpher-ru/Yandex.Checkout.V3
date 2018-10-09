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
        public decimal value { get; set; }

        /// <summary>
        /// Three letter currency code (ex: RUB)
        /// </summary>
        public string currency { get; set; } = "RUB";
    }
}
