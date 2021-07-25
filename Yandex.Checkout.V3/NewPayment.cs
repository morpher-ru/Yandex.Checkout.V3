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
        public Amount Amount { get; set; }

        /// <summary>
        /// Описание транзакции, которое вы увидите в личном кабинете Яндекс.Кассы, а пользователь — при оплате.
        /// Например: "Оплата заказа №72 для user@yandex.ru"
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Автоматический прием поступившего платежа.
        /// </summary>
        public bool? Capture { get; set; }

        /// <summary>
        /// Данные, необходимые для инициации выбранного сценария подтверждения платежа пользователем.
        /// </summary>
        public Confirmation Confirmation { get; set; } = new();

        /// <summary>
        /// Дополнительные данные, которые можно передать вместе с запросом
        /// и получить в ответе от Яндекс.Кассы для реализации внутренней логики. 
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// Чек для проведения платежа по ФЗ-54 <see cref="V3.Receipt"/>
        /// </summary>
        public Receipt Receipt { get; set; }

        public Recipient Recipient { get; set; }

        public string PaymentToken { get; set; }

        public string PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethodData { get; set; }

        public bool? SavePaymentMethod { get; set; }

        public string ClientIp { get; set; }

        public Airline Airline { get; set; }

        /// <summary>
        /// Информация о сделке
        /// </summary>
        public PaymentDeal Deal { get; set; }
    }
}
