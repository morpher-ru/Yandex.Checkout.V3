// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Yandex.Checkout.V3;

/// <summary>
/// Фильтр для запроса списка чеков 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class ReceiptFilter
{
    /// <summary>
    /// Фильтр по времени создания 
    /// </summary>
    public DateFilter CreatedAt { get; set; }

    /// <summary>
    /// Фильтр по статусу чека
    /// </summary>
    public ReceiptStatus? Status { get; set; }

    /// <summary>
    /// Фильтр по идентификатору платежа (получить все чеки для указанного платежа)
    /// </summary>
    public string PaymentId { get; set; }

    /// <summary>
    /// Фильтр по идентификатору возврата (получить все чеки для указанного возврата)
    /// </summary>
    public string RefundId { get; set; }
}
