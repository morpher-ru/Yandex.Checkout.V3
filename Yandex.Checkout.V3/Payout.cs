namespace Yandex.Checkout.V3;

// ReSharper disable once ClassNeverInstantiated.Global
public class Payout
{
    public string Id { get; set; }

    public Amount Amount { get; set; }

    public PayoutStatus Status { get; set; }

    public PayoutDestination PayoutDestination { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public PayoutDeal Deal { get; set; }

    public IDictionary<string, string> Metadata { get; set; }

    public CancellationDetails CancellationDetails { get; set; }

    public bool Test { get; set; }
}
