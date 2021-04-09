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
        public Amount Amount { get; init; }

        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        [JsonRequired]
        public string PaymentId { get; init; }

        /// <summary>
        /// Чек, для проведения возврата по 54-ФЗ <see cref="V3.Receipt"/>
        /// </summary>
        public Receipt Receipt { get; init; }

        public string Description { get; init; }
    }
}
