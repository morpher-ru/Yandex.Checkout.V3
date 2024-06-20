namespace Yandex.Checkout.V3;

/// <summary>
/// Состояние отправки чека по 54-ФЗ
/// </summary>
[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum ReceiptRegistrationStatus
{
    Pending = 1,
    Succeeded,
    Canceled
}
