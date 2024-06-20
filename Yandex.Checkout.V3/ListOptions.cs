namespace Yandex.Checkout.V3;

// ReSharper disable once ClassNeverInstantiated.Global
public class ListOptions
{
    /// <summary>
    /// Количество элементов списка, получаемых за один запрос.
    /// В оригинальной документации это называется limit.
    /// От 1 до 100, по умолчанию 10.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int? PageSize { get; set; }
}
