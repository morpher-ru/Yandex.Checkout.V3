using System;

namespace Yandex.Checkout.V3
{
    /// <inheritdoc />
    /// <summary>
    /// Информация о платеже.
    /// </summary>
    public class Payment : NewPayment
    {
        public string Id { get; set; }
        
        public PaymentStatus Status { get; set; }
        
        public bool Paid { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public ReceiptRegistrationStatus? ReceiptRegistration { get; set; }
        
        public DateTime? CapturedAt { get; set; }
        
        public DateTime? ExpiresAt { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }
        
        public bool? Test { get; set; }
        
        public Amount RefundedAmount { get; set; }
        
        public CancellationDetails CancellationDetails { get; set; }
        
        public AuthorizationDetails AuthorizationDetails { get; set; }
        
        public PayoutDestination PayoutDestination { get; set; }
    }
}
