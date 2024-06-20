namespace Yandex.Checkout.V3;

[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum RefundStatus
{
    Pending,
    Succeeded,
    Canceled
}
