namespace Yandex.Checkout.V3;

/// <summary>
/// Фильтры по дате-времени.
/// </summary>
public class DateFilter
{
    /// <summary>
    /// Время должно быть больше указанного значения или равно ему («с такого-то момента включительно»)
    /// </summary>
    public DateTimeOffset? Gte { get; set; }

    /// <summary>
    /// Время должно быть больше указанного значения («с такого-то момента, не включая его»)
    /// </summary>
    public DateTimeOffset? Gt { get; set; }

    /// <summary>
    /// Время должно быть меньше указанного значения или равно ему («по такой-то момент включительно»)
    /// </summary>
    public DateTimeOffset? Lte { get; set; }

    /// <summary>
    /// Время должно быть меньше указанного значения («по такой-то момент, не включая его»)
    /// </summary>
    public DateTimeOffset? Lt { get; set; }
}