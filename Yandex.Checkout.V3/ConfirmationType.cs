using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConfirmationType
    {
        /// <summary>Вам нужно перенаправить пользователя на confirmation_url, полученный в платеже.</summary>
        [EnumMember(Value = "redirect")]
        Redirect,

        /// <summary>Для подтверждения платежа пользователю необходимо совершить действия во внешней системе (например, ответить на смс). От вас требуется только сообщить пользователю о дальнейших шагах.</summary>
        [EnumMember(Value = "external")]
        External,
        
        /// <summary>Действия, необходимые для подтверждения платежа, будут зависеть от способа оплаты, который пользователь выберет в виджете Яндекс.Кассы. Подтверждение от пользователя получит Яндекс.Касса — вам необходимо только встроить виджет к себе на страницу.</summary>
        [EnumMember(Value = "embedded")]
        Embedded,
        
        /// <summary>Для подтверждения платежа пользователю необходимо просканировать QR-код. От вас требуется сгенерировать QR-код, используя любой доступный инструмент, и отобразить его на странице оплаты.</summary>
        [EnumMember(Value = "qr")]
        QR,

        /// <summary>Для подтверждения платежа пользователю необходимо открыть URL из confirmation_url в приложении (именно открыть, а не перейти во встроенном в приложении браузере), после чего он будет перенаправлен в приложение СберБанк Онлайн для подтверждения платежа.</summary>
        [EnumMember(Value = "mobile_application")]
        MobileApplication
    }
}
