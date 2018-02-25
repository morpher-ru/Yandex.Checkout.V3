using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    public class NewPayment
    {
        /// <summary>
        /// Сумма платежа. 
        /// Иногда партнеры Яндекс.Кассы берут с полльзователя дополнительную комиссию, которая не входит в эту сумму.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Amount amount { get; set; }

        /// <summary>
        /// Описание транзакции, которое вы увидите в личном кабинете Яндекс.Кассы, а пользователь — при оплате.
        /// Например: "Оплата заказа №72 для user@yandex.ru"
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }

        /// <summary>
        /// Автоматический прием поступившего платежа.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? capture { get; set; }

        /// <summary>
        /// Данные, необходимые для инициации выбранного сценария подтверждения платежа пользователем.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Confirmation confirmation = new Confirmation();
    }

    public class Payment : NewPayment
    {
        public Guid id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus status { get; set; }

        public bool paid { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? created_at { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? expires_at { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PaymentMethod payment_method { get; set; }
    }
}
