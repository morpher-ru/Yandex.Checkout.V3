namespace Yandex.Checkout.V3;

/// <summary>
/// Фильтр для запроса списка платежей 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class PaymentFilter
{
    /// <summary>
    /// Фильтр по времени создания платежей
    /// </summary>
    public DateFilter CreatedAt { get; set; }
        
    /// <summary>
    /// Фильтр по времени подтверждения платежей
    /// </summary>
    public DateFilter CapturedAt { get; set; }
        
    /// <summary>
    /// Фильтр по коду способа оплаты 
    /// </summary>
    public string PaymentMethod { get; set; }
        
    /// <summary>
    /// Фильтр по статусу платежа:
    /// https://yookassa.ru/developers/payment-acceptance/getting-started/payment-process#lifecycle
    /// </summary>
    public PaymentStatus Status { get; set; }
}