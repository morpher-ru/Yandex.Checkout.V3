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
        /// Номер телефона плательщика
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Электронная почта плательщика
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пoзиции чека, <see cref="ReceiptItem"/>
        /// </summary>
        public List<ReceiptItem> Items { get; set; } = new List<ReceiptItem>();

        /// <summary>
        /// Система налогообложения, <see cref="TaxSystem"/>
        /// </summary>
        public TaxSystem? TaxSystemCode { get; set; }
    }
}
