namespace Yandex.Checkout.V3;

[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum ConfirmationType
{
    /// <summary>
    /// Вам нужно перенаправить пользователя на confirmation_url, полученный в платеже.
    /// </summary>
    Redirect,

    /// <summary>
    /// Для подтверждения платежа пользователю необходимо совершить действия
    /// во внешней системе (например, ответить на смс).
    /// От вас требуется только сообщить пользователю о дальнейших шагах.</summary>
    External,
        
    /// <summary>
    /// Действия, необходимые для подтверждения платежа, будут зависеть от
    /// способа оплаты, который пользователь выберет в виджете Яндекс.Кассы.
    /// Подтверждение от пользователя получит Яндекс.Касса —
    /// вам необходимо только встроить виджет к себе на страницу.
    /// </summary>
    Embedded,
        
    /// <summary>
    /// Для подтверждения платежа пользователю необходимо просканировать QR-код.
    /// От вас требуется сгенерировать QR-код, используя любой доступный инструмент,
    /// и отобразить его на странице оплаты.
    /// </summary>
    QR,

    /// <summary>
    /// Для подтверждения платежа необходимо перенаправить пользователя на confirmation_url, полученный в платеже.
    /// </summary>
    /// <remarks>
    /// Подробнее см. https://yookassa.ru/developers/payment-methods/other/sberpay#create-payment-mobile_application
    /// </remarks>
    MobileApplication
}
