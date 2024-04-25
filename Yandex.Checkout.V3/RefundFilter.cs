namespace Yandex.Checkout.V3
{
    // ReSharper disable once ClassNeverInstantiated.Global
    
    /// <summary>
    /// Фильтр возвратов
    /// </summary>
    public class RefundFilter : BaseFilter
    {
        public string PaymentId { get; set; }

        public RefundStatus Status { get; set; }
    }
}
