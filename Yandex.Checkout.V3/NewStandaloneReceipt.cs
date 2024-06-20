namespace Yandex.Checkout.V3;

/// <summary>
/// Данные для создания чека отдельно от платежа или возврата
/// </summary>
/// <remarks>
/// See https://yookassa.ru/developers/api#create_receipt
/// </remarks>
// ReSharper disable once ClassNeverInstantiated.Global
public class NewStandaloneReceipt : NewReceipt
{
    /// <summary>
    /// Тип чека в онлайн-кассе
    /// </summary>
    public ReceiptType Type { get; set; }

    /// <summary>
    /// Идентификатор платежа в ЮKassa для отображения информации о чеке в личном кабинете, на платеж не влияет.
    /// </summary>
    public string PaymentId { get; set; }

    /// <summary>
    /// Идентификатор возврата в ЮKassa для отображения информации о чеке в личном кабинете.
    /// </summary>
    public string RefundId { get; set; }
}
