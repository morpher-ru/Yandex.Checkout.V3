namespace Yandex.Checkout.V3;

[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum CancellationParty
{
    /// <summary>
    /// ЮKassa
    /// </summary>
    YooMoney,

    /// <summary>
    /// Продавец товаров и услуг
    /// </summary>
    Merchant,

    /// <summary>
    /// Любые участники процесса платежа, кроме ЮKassa и вас (например, эмитент банковской карты)
    /// </summary>
    PaymentNetwork,

    /// <summary>
    /// Любые участники процесса возврата, кроме ЮKassa и вас (например, эмитент банковской карты)
    /// </summary>
    RefundNetwork
}
