using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    /// <inheritdoc />
    /// <summary>
    /// Информация о возврате
    /// </summary>
    public class Refund : NewRefund
    {
        /// <summary>
        /// Идентификатор возврата
        /// </summary>
        public string Id { get; init; }

        /// <summary>
        /// Статус
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus Status { get; init; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; init; }

        public ReceiptRegistrationStatus? ReceiptRegistration { get; init; }
    }
}
