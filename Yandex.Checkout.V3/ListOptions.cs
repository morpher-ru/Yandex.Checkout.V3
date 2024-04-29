using System.Threading;

namespace Yandex.Checkout.V3;

public class ListOptions
{
    public CancellationToken CancellationToken { get; set; }
        
    public string IdempotenceKey { get; set; }
        
    /// <summary>
    /// Количество элементов списка, получаемых за один запрос.
    /// В оригинальной документации это называется limit.
    /// От 1 до 100, по умолчанию 10.
    /// </summary>
    public int? PageSize { get; set; }
}