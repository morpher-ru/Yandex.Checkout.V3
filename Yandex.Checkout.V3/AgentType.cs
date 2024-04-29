namespace Yandex.Checkout.V3;

/// <summary>
/// Тип посредника
/// </summary>
[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum AgentType
{
    /// <summary>
    /// Банковский платежный агент
    /// </summary>
    BankingPaymentAgent,

    /// <summary>
    /// Банковский платежный субагент
    /// </summary>
    BankingPaymentSubagent,

    /// <summary>
    /// Платежный агент
    /// </summary>
    PaymentAgent,

    /// <summary>
    /// Платежный субагент
    /// </summary>
    PaymentSubagent,

    /// <summary>
    /// Поверенный
    /// </summary>
    Attorney,

    /// <summary>
    /// Комиссионер
    /// </summary>
    Commissioner,

    /// <summary>
    /// Агент
    /// </summary>
    Agent
}
