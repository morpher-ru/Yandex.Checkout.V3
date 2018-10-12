using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Оплата с чеком по ФЗ-54
    /// </summary>
    public class PaymentWithReceipt : NewPayment
    {
        /// <summary>
        /// Чек, <see cref="V3.Receipt"/>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Receipt Receipt { get; set; }
    }
}