namespace Yandex.Checkout.V3;

/// <summary>
/// Объект чека (Receipt) содержит актуальную информацию о чеке, созданном для платежа или возврата
/// </summary>
/// <remarks>
/// See https://yookassa.ru/developers/api#receipt_object
/// </remarks>
// ReSharper disable once ClassNeverInstantiated.Global
public class Receipt
{
    /// <summary>
    /// Идентификатор чека.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Тип чека в онлайн-кассе
    /// </summary>
    public ReceiptType Type { get; set; }

    /// <summary>
    /// Идентификатор платежа, для которого был сформирован чек
    /// </summary>
    public string PaymentId { get; set; }

    /// <summary>
    /// Идентификатор возврата, для которого был сформирован чек. Отсутствует в чеке платежа
    /// </summary>
    public string RefundId { get; set; }

    /// <summary>
    /// Статус доставки данных для чека в онлайн-кассу
    /// </summary>
    public ReceiptStatus Status { get; set; }

    /// <summary>
    /// Номер фискального документа
    /// </summary>
    public string FiscalDocumentNumber { get; set; }

    /// <summary>
    /// Номер фискального накопителя в кассовом аппарате
    /// </summary>
    public string FiscalStorageNumber { get; set; }

    /// <summary>
    /// Фискальный признак чека. Формируется фискальным накопителем на основе данных, переданных для регистрации чека
    /// </summary>
    public string FiscalAttribute { get; set; }

    /// <summary>
    /// Дата и время формирования чека в фискальном накопителе
    /// </summary>
    public DateTime? RegisteredAt { get; set; }

    /// <summary>
    /// Идентификатор чека в онлайн-кассе. Присутствует, если чек удалось зарегистрировать
    /// </summary>
    public string FiscalProviderId { get; set; }

    /// <summary>
    /// Позиции чека, <see cref="ReceiptItem"/>
    /// Список товаров в чеке (не более 100 товаров).
    /// </summary>
    public List<ReceiptItem> Items { get; set; } = new();

    /// <summary>
    /// Перечень совершенных расчетов.
    /// </summary>
    public List<Settlement> Settlements { get; set; } = new();

    /// <summary>
    /// Идентификатор магазина, от имени которого нужно отправить чек
    /// </summary>
    public string OnBehalfOf { get; set; }

    /// <summary>
    /// Система налогообложения, <see cref="TaxSystem"/>
    /// </summary>
    public TaxSystem? TaxSystemCode { get; set; }

    /// <summary>
    /// Отраслевой реквизит чека (тег в 54 ФЗ — 1261).
    /// </summary>
    public ReceiptIndustryDetails ReceiptIndustryDetails { get; set; }

    /// <summary>
    /// Операционный реквизит чека (тег в 54 ФЗ — 1270).
    /// </summary>
    public ReceiptOperationalDetails ReceiptOperationalDetails { get; set; }
}
