namespace Yandex.Checkout.V3;

/// <inheritdoc />
/// <summary>
/// Информация о возврате
/// </summary>
/// <remarks>
/// See https://yookassa.ru/developers/api#refund_object
/// </remarks>
public class Refund : NewRefund
{
    /// <summary>
    /// Идентификатор возврата
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public RefundStatus Status { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    public ReceiptRegistrationStatus? ReceiptRegistration { get; set; }

    public RefundDeal Deal { get; set; }
}
