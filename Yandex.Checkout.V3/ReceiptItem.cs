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

        /// <summary>
        /// Признак предмета расчета, <see cref="V3.PaymentSubject"/>
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentSubject? PaymentSubject { get; set; }

        /// <summary>
        /// Признак способа расчета <see cref="V3.PaymentMode"/>
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentMode? PaymentMode { get; set; }
        
        /// <summary>
        /// Код товара — уникальный номер, который присваивается экземпляру товара при маркировке.
        /// </summary>
        /// <remarks>
        /// See https://yookassa.ru/developers/api#create_receipt_items_product_code
        /// </remarks>
        public string ProductCode { get; set; }

        /// <summary>
        /// Код страны происхождения товара по общероссийскому классификатору стран мира.
        /// Пример: RU.
        /// </summary>
        public string CountryOfOriginCode { get; set; }

        /// <summary>
        /// Номер таможенной декларации (от 1 до 32 символов).
        /// </summary>
        public string CustomsDeclarationNumber { get; set; }

        /// <summary>
        /// Сумма акциза товара с учетом копеек.
        /// Десятичное число с точностью до 2 символов после точки.
        /// </summary>
        public string Excise { get; set; }
        
        /// <summary>
        /// Тип посредника, реализующего товар или услугу
        /// </summary>
        public AgentType? AgentType { get; set; }

        /// <summary>
        /// Информация о поставщике товара или услуги
        /// </summary>
        public Supplier Supplier { get; set; }
    }
}
