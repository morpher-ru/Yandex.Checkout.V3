namespace Yandex.Checkout.V3;

public class ReceiptBase
{
    /// <summary>
    /// Список товаров в чеке (не более 100 товаров).
    /// </summary>
    public List<ReceiptItem> Items { get; set; } = new();

    /// <summary>
    /// Система налогообложения, <see cref="TaxSystem"/>
    /// </summary>
    public TaxSystem? TaxSystemCode { get; set; }

    /// <summary>
    /// Отраслевой реквизит чека (тег в 54 ФЗ — 1261). Нужно передавать, если используете ФФД 1.2.
    /// </summary>
    public ReceiptIndustryDetails [] ReceiptIndustryDetails { get; set; }

    /// <summary>
    /// Операционный реквизит чека (тег в 54 ФЗ — 1270). Нужно передавать, если используете ФФД 1.2.
    /// </summary>
    public ReceiptOperationalDetails ReceiptOperationalDetails { get; set; }

    /// <summary>
    /// Перечень совершенных расчетов.
    /// </summary>
    public List<Settlement> Settlements { get; set; } = new();

    /// <summary>
    /// Идентификатор магазина, от имени которого нужно отправить чек
    /// </summary>
    public string OnBehalfOf { get; set; }
}
