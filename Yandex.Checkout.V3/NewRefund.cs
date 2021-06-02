using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Данные для оформления возвората
    /// </summary>
    public class NewRefund
    {
        /// <summary>
        /// Сумма к возврату
        /// </summary>
        [JsonRequired]
        public Amount Amount { get; set; }

        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        [JsonRequired]
        public string PaymentId { get; set; }

        /// <summary>
        /// Чек для проведения возврата по 54-ФЗ <see cref="V3.Receipt"/>
        /// </summary>
        public Receipt Receipt { get; set; }

        public string Description { get; set; }
    }
}
