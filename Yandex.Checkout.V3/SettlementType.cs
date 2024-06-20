namespace Yandex.Checkout.V3;

[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum SettlementType
{
    /// <summary>
    /// Безналичный расчет
    /// </summary>
    Cashless,

    /// <summary>
    /// Предоплата (аванс)
    /// </summary>
    Prepayment,

    /// <summary>
    /// Постоплата (кредит)
    /// </summary>
    Postpayment,

    /// <summary>
    /// Встречное предоставление
    /// </summary>
    Consideration,

    /// <summary>
    /// Выплата
    /// </summary>
    Payout
}
