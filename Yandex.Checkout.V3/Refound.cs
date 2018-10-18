using System;

namespace Yandex.Checkout.V3
{
    /// <inheritdoc />
    /// <summary>
    /// Информация о возврате
    /// </summary>
    public class Refound : NewRefound
    {
        /// <summary>
        /// Идентификатор возврата
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
