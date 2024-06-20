namespace Yandex.Checkout.V3;

public class ReceiptIndustryDetails
{
    /// <summary>
    /// Идентификатор федерального органа исполнительной власти (тег в 54 ФЗ — 1262).
    /// </summary>
    public string FederalId { get; set; }
    
    /// <summary>
    /// Дата документа основания (тег в 54 ФЗ — 1263).
    /// </summary>
    public DateTime DocumentDate { get; set; }

    /// <summary>
    /// Номер нормативного акта федерального органа исполнительной власти,
    /// регламентирующего порядок заполнения реквизита «значение отраслевого реквизита»
    /// (тег в 54 ФЗ — 1264).
    /// </summary>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Значение отраслевого реквизита (тег в 54 ФЗ — 1265).
    /// </summary>
    public string Value { get; set; }
}
