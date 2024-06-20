namespace Yandex.Checkout.V3;

// ReSharper disable once ClassNeverInstantiated.Global
public class AdditionalUserProps
{
    /// <summary>
    /// Наименование дополнительного реквизита пользователя (тег в 54 ФЗ — 1085). Не более 64 символов.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Значение дополнительного реквизита пользователя (тег в 54 ФЗ — 1086). Не более 234 символов.
    /// </summary>
    public string Value { get; set; }
}
