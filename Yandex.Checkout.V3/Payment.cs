using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Исходные данные для создания платежа.
    /// </summary>
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

        /// <summary>
        /// Дополнительные данные, которые можно передать вместе с запросом
        /// и получить в ответе от Яндекс.Кассы для реализации внутренней логики. 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> metadata;
    }

    /// <summary>
    /// Информация о платеже.
    /// </summary>
    public class Payment : NewPayment
    {
        public Guid id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus status { get; set; }

        public bool paid { get; set; }

        public DateTime created_at { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? expires_at { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PaymentMethod payment_method { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? test { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Amount refunded_amount { get; set; }
    }

    /// <summary>
    /// Оплата с чеком по ФЗ-54
    /// </summary>
    public class PaymentWithReceipt : NewPayment
    {
        /// <summary>
        /// Чек, <see cref="Receipt"/>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Receipt receipt { get; set; }
    }

    /// <summary>
    /// Чек
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Номер телефона плетельщика
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Пoзиции чека, <see cref="ReceiptItem"/>
        /// </summary>
        public List<ReceiptItem> items { get; set; } = new List<ReceiptItem>();

        /// <summary>
        /// Система налогообложения, <see cref="TaxSystem"/>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TaxSystem? tax_system_code { get; set; }
    }

    /// <summary>
    /// Позиция чека
    /// <seealso cref="Receipt"/>
    /// </summary>
    public class ReceiptItem
    {
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string description { get; set; } 

        /// <summary>
        /// Количество
        /// </summary>
        public decimal quantity { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public Amount amount { get; set; }

        /// <summary>
        /// Код налога, <see cref="VatCode"/>
        /// </summary>
        public VatCode vat_code { get; set; }
    }
}
