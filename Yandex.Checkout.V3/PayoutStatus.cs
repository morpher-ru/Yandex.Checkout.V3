namespace Yandex.Checkout.V3;

[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum PayoutStatus
{
    Pending,
    Succeeded,
    Canceled
}
