using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Данные для формирования чека зачета предоплаты
    /// </summary>
    public class SettlementReceipt
    {
        public string Id { get; init; }

        /// <summary>
        /// Тип чека в онлайн-кассе
        /// </summary>
        public SettlementReceiptType Type { get; init; }

        /// <summary>
        /// Идентификатор платежа в ЮKassa для отображения информации о чеке в личном кабинете, на платеж не влияет.
        /// </summary>
        public string PaymentId { get; init; }

        /// <summary>
        /// Идентификатор возврата в ЮKassa для отображения информации о чеке в личном кабинете.
        /// </summary>
        public string RefundId { get; init; }

        /// <summary>
        /// Информация о пользователе.
        /// </summary>
        public Customer Customer { get; init; }

        /// <summary>
        /// Список товаров в чеке (не более 100 товаров).
        /// </summary>
        public List<ReceiptItem> Items { get; init; } = new();

        /// <summary>
        /// Система налогообложения, <see cref="TaxSystem"/>
        /// </summary>
        public TaxSystem? TaxSystemCode { get; init; }

        /// <summary>
        /// Формирование чека в онлайн-кассе сразу после создания объекта чека
        /// </summary>
        public bool Send { get; init; }

        /// <summary>
        /// Перечень совершенных расчетов.
        /// </summary>
        public List<Settlement> Settlements { get; init; } = new();

        /// <summary>
        /// Идентификатор магазина, от имени которого нужно отправить чек
        /// </summary>
        public string OnBehalfOf { get; init; }
    }
}
