using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    /// <inheritdoc />
    /// <summary>
    /// Информация о платеже.
    /// </summary>
    public class Payment : NewPayment
    {
        public string Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus Status { get; set; }

        public bool Paid { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ReceiptRegistrationStatus? ReceiptRegistration { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CapturedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ExpiresAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Test { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Amount RefundedAmount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CancellationDetails CancellationDetails { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AuthorizationDetails AuthorizationDetails { get; set; }
    }
}
