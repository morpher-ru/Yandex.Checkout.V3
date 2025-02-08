namespace Yandex.Checkout.V3;

/// <summary>
/// Виды платежных поручений.
/// </summary>
[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum PaymentOrderType
{
    /// <summary>
    /// Оплата ЖКУ.
    /// </summary>
    Utilities
}
