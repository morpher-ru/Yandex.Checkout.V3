using System;

namespace Yandex.Checkout.V3
{
    /// <inheritdoc />
    /// <summary>
    /// Информация о платеже.
    /// </summary>
    public class Payment : NewPayment
    {
        public string Id { get; init; }
        public PaymentStatus Status { get; init; }
        public bool Paid { get; init; }
        public DateTime CreatedAt { get; init; }
        public ReceiptRegistrationStatus? ReceiptRegistration { get; init; }
        public DateTime? CapturedAt { get; init; }
        public DateTime? ExpiresAt { get; init; }
        public PaymentMethod PaymentMethod { get; init; }
        public bool? Test { get; init; }
        public Amount RefundedAmount { get; init; }
        public CancellationDetails CancellationDetails { get; init; }
        public AuthorizationDetails AuthorizationDetails { get; init; }
    }
}
