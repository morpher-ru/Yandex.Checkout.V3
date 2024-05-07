namespace Yandex.Checkout.V3;

/// <summary>
/// Данные для создания чека вместе с платежом или возвратом
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class NewReceipt
{
    /// <summary>
    /// Информация о пользователе. Необходимо указать как минимум контактные данные:
    /// электронную почту (customer.email <see cref="V3.Customer.Email"/>) или 
    /// номер телефона (customer.phone <see cref="V3.Customer.Phone"/>).
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    /// Список товаров в чеке (не более 100 товаров).
    /// </summary>
    public List<ReceiptItem> Items { get; set; } = new();

    /// <summary>
    /// Формирование чека в онлайн-кассе сразу после создания объекта чека.
    /// Сейчас допускается только значение True.
    /// </summary>
    public bool Send { get; set; }

    /// <summary>
    /// Система налогообложения, <see cref="TaxSystem"/>
    /// </summary>
    public TaxSystem? TaxSystemCode { get; set; }

    /// <summary>
    /// Дополнительный реквизит пользователя (тег в 54 ФЗ — 1084).
    /// </summary>
    /// <remarks>
    /// Можно передавать, если вы отправляете данные для формирования чека
    /// по сценарию «Сначала платеж, потом чек».
    /// </remarks>
    public AdditionalUserProps AdditionalUserProps { get; set; }

    /// <summary>
    /// Отраслевой реквизит чека (тег в 54 ФЗ — 1261). Нужно передавать, если используете ФФД 1.2.
    /// </summary>
    public ReceiptIndustryDetails [] ReceiptIndustryDetails { get; set; }

    /// <summary>
    /// Операционный реквизит чека (тег в 54 ФЗ — 1270). Нужно передавать, если используете ФФД 1.2.
    /// </summary>
    public ReceiptOperationalDetails ReceiptOperationalDetails { get; set; }
    
    /// <summary>
    /// Перечень совершенных расчетов.
    /// </summary>
    public List<Settlement> Settlements { get; set; } = new();

    /// <summary>
    /// Идентификатор магазина, от имени которого нужно отправить чек
    /// </summary>
    public string OnBehalfOf { get; set; }
}
