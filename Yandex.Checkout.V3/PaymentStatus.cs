namespace Yandex.Checkout.V3;

[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum PaymentStatus
{
    Pending,
    WaitingForCapture,
    Succeeded,
    Canceled
}
