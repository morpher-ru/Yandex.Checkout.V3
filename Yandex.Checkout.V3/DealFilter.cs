namespace Yandex.Checkout.V3;

// ReSharper disable once ClassNeverInstantiated.Global
public class DealFilter
{
    public DateFilter CreatedAt { get; set; }
    public DateFilter ExpiresAt { get; set; }
    public DealStatus? Status { get; set; }
        
    /// <summary>
    /// Фильтр по описанию сделки — параметру description
    /// (например, идентификатор сделки на стороне вашей интернет-площадки в ЮKassa,
    /// идентификатор покупателя или продавца).
    /// От 4 до 128 символов.
    /// </summary>
    public string FullTextSearch { get; set; }
}