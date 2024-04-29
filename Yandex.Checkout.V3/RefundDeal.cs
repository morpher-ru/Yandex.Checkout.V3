namespace Yandex.Checkout.V3;

// ReSharper disable once ClassNeverInstantiated.Global
public class RefundDeal
{
    /// <summary>
    /// Id сделки.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Перечень совершенных расчетов.
    /// </summary>
    public List<Settlement> RefundSettlements { get; set; } = new();
}
