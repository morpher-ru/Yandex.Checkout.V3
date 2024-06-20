namespace Yandex.Checkout.V3;

/// <summary>
/// Данные для создания чека вместе с платежом или возвратом
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class NewReceipt : ReceiptBase
{
    /// <summary>
    /// Информация о пользователе. Необходимо указать как минимум контактные данные:
    /// электронную почту (customer.email <see cref="V3.Customer.Email"/>) или 
    /// номер телефона (customer.phone <see cref="V3.Customer.Phone"/>).
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    /// Формирование чека в онлайн-кассе сразу после создания объекта чека.
    /// Сейчас допускается только значение True.
    /// </summary>
    public bool Send { get; set; }

    /// <summary>
    /// Дополнительный реквизит пользователя (тег в 54 ФЗ — 1084).
    /// </summary>
    /// <remarks>
    /// Можно передавать, если вы отправляете данные для формирования чека
    /// по сценарию «Сначала платеж, потом чек».
    /// </remarks>
    public AdditionalUserProps AdditionalUserProps { get; set; }
}
