using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        public string Description { get; init; } 

        /// <summary>
        /// Количество
        /// </summary>
        public decimal Quantity { get; init; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public Amount Amount { get; init; }

        /// <summary>
        /// Код налога, <see cref="V3.VatCode"/>
        /// </summary>
        public VatCode VatCode { get; init; }

        /// <summary>
        /// Признак предмета расчета, <see cref="V3.PaymentSubject"/>
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentSubject? PaymentSubject { get; init; }

        /// <summary>
        /// Признак способа расчета <see cref="V3.PaymentMode"/>
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentMode? PaymentMode { get; init; }

        /// <summary>
        /// Тип посредника, реализующего товар или услугу
        /// </summary>
        public AgentType? AgentType { get; init; }
    }
}
