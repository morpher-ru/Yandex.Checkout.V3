using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Исходные данные для создания платежа.
    /// </summary>
    public class NewPayment
    {
        /// <summary>
        /// Сумма платежа. 
        /// Иногда партнеры Яндекс.Кассы берут с пользователя дополнительную комиссию, которая не входит в эту сумму.
        /// </summary>
        public Amount Amount { get; init; }

        /// <summary>
        /// Описание транзакции, которое вы увидите в личном кабинете Яндекс.Кассы, а пользователь — при оплате.
        /// Например: "Оплата заказа №72 для user@yandex.ru"
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Автоматический прием поступившего платежа.
        /// </summary>
        public bool? Capture { get; init; }

        /// <summary>
        /// Данные, необходимые для инициации выбранного сценария подтверждения платежа пользователем.
        /// </summary>
        public Confirmation Confirmation { get; init; } = new();

        /// <summary>
        /// Дополнительные данные, которые можно передать вместе с запросом
        /// и получить в ответе от Яндекс.Кассы для реализации внутренней логики. 
        /// </summary>
        public Dictionary<string, string> Metadata { get; init; }

        /// <summary>
        /// Чек для проведения платежа по ФЗ-54 <see cref="V3.Receipt"/>
        /// </summary>
        public Receipt Receipt { get; init; }

        public Recipient Recipient { get; init; }

        public string PaymentToken { get; init; }

        public string PaymentMethodId { get; init; }

        public PaymentMethod PaymentMethodData { get; init; }

        public bool? SavePaymentMethod { get; init; }

        public string ClientIp { get; init; }

        public Airline Airline { get; init; }
    }
}
