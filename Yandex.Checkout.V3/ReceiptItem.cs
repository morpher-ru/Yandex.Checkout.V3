namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Позиция чека
    /// <seealso cref="Receipt"/>
    /// </summary>
    public class ReceiptItem
    {
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Description { get; set; } 

        /// <summary>
        /// Количество
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public Amount Amount { get; set; }

        /// <summary>
        /// Код налога, <see cref="V3.VatCode"/>
        /// </summary>
        public VatCode VatCode { get; set; }
    }
}