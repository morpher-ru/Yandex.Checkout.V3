namespace Yandex.Checkout.V3;
// ReSharper disable once ClassNeverInstantiated.Global
/// <summary>
/// Фильтр возвратов
/// </summary>
public class RefundFilter
{
    /// <summary>
    /// Фильтр по времени создания 
    /// </summary>
    public DateFilter CreatedAt { get; set; }

    public string PaymentId { get; set; }

    public RefundStatus? Status { get; set; }
}