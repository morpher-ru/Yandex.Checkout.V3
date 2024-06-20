namespace Yandex.Checkout.V3;

// ReSharper disable once ClassNeverInstantiated.Global
public class ReceiptOperationalDetails
{
    /// <summary>
    /// Идентификатор операции (тег в 54 ФЗ — 1271). Число от 0 до 255.
    /// </summary>
    public int OperationId { get; set; }

    /// <summary>
    /// Данные операции (тег в 54 ФЗ — 1272).
    /// </summary>
    public string Value { get; set; }
    
    /// <summary>
    /// Время создания операции (тег в 54 ФЗ — 1273).
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
