using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Чек
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Номер телефона плетельщика
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Пoзиции чека, <see cref="ReceiptItem"/>
        /// </summary>
        public List<ReceiptItem> Items { get; set; } = new List<ReceiptItem>();

        /// <summary>
        /// Система налогообложения, <see cref="TaxSystem"/>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TaxSystem? TaxSystemCode { get; set; }
    }
}