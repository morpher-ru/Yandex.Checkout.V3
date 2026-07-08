// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Yandex.Checkout.V3;

/// <remarks>
/// See https://yookassa.ru/developers/payment-acceptance/after-the-payment/refunds#declined-refunds-cancellation-details-party
/// </remarks>
public class CancellationDetails
{
    public CancellationParty Party { get; set; }
    public CancellationReason Reason { get; set; }
}
