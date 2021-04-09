using System.Collections.Generic;

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
        public string Phone { get; init; }

        /// <summary>
        /// Электронная почта плательщика
        /// </summary>
        public string Email { get; init; }

        /// <summary>
        /// Пoзиции чека, <see cref="ReceiptItem"/>
        /// </summary>
        public List<ReceiptItem> Items { get; init; } = new();

        /// <summary>
        /// Система налогообложения, <see cref="TaxSystem"/>
        /// </summary>
        public TaxSystem? TaxSystemCode { get; init; }
    }
}
