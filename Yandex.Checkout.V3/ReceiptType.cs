namespace Yandex.Checkout.V3;

[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum ReceiptType
{
    /// <summary>
    /// Приход
    /// </summary>
    Payment,

    /// <summary>
    /// Возврат прихода
    /// </summary>
    Refund
}
