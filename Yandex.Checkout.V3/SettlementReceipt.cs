using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Данные для формирования чека
    /// </summary>
    /// <remarks>
    /// Используется для создания чека отдельно от платежа или возврата,
    /// см. 
    /// See https://yookassa.ru/developers/api#create_receipt
    /// </remarks>
    public class SettlementReceipt
    {
        public string Id { get; set; }

        /// <summary>
        /// Тип чека в онлайн-кассе
        /// </summary>
        public SettlementReceiptType Type { get; set; }

        /// <summary>
        /// Идентификатор платежа в ЮKassa для отображения информации о чеке в личном кабинете, на платеж не влияет.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Идентификатор возврата в ЮKassa для отображения информации о чеке в личном кабинете.
        /// </summary>
        public string RefundId { get; set; }

        /// <summary>
        /// Информация о пользователе.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Список товаров в чеке (не более 100 товаров).
        /// </summary>
        public List<ReceiptItem> Items { get; set; } = new();

        /// <summary>
        /// Система налогообложения, <see cref="TaxSystem"/>
        /// </summary>
        public TaxSystem? TaxSystemCode { get; set; }

        /// <summary>
        /// Формирование чека в онлайн-кассе сразу после создания объекта чека
        /// </summary>
        public bool Send { get; set; }

        /// <summary>
        /// Перечень совершенных расчетов.
        /// </summary>
        public List<Settlement> Settlements { get; set; } = new();

        /// <summary>
        /// Идентификатор магазина, от имени которого нужно отправить чек
        /// </summary>
        public string OnBehalfOf { get; set; }
    }
}
