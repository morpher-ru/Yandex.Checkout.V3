using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CancellationReason
    {
        /// <summary>
        /// Не пройдена аутентификация по 3-D Secure. При новой попытке оплаты пользователю следует пройти аутентификацию, использовать другое платежное средство или обратиться в банк за уточнениями
        /// </summary>
        [EnumMember(Value = "3d_secure_failed")]
        ThreeDSecureFailed,
        /// <summary>
        /// Оплата данным платежным средством отклонена по неизвестным причинам. Пользователю следует обратиться в организацию, выпустившую платежное средство
        /// </summary>
        [EnumMember(Value = "call_issuer")]
        CallIssuer,
        /// <summary>
        /// Платеж отменен по API при оплате в две стадии
        /// </summary>
        [EnumMember(Value = "canceled_by_merchant")]
        CanceledByMerchant,
        /// <summary>
        /// Истек срок действия банковской карты. При новой попытке оплаты пользователю следует использовать другое платежное средство
        /// </summary>
        [EnumMember(Value = "card_expired")]
        CardExpired,
        /// <summary>
        /// Нельзя заплатить банковской картой, выпущенной в этой стране. При новой попытке оплаты пользователю следует использовать другое платежное средство.
        /// </summary>
        [EnumMember(Value = "country_forbidden")]
        CountryForbidden,
        /// <summary>
        /// Истек срок списания оплаты у двухстадийного платежа. Если вы еще хотите принять оплату, повторите платеж с новым ключом идемпотентности и спишите деньги после подтверждения платежа пользователем
        /// </summary>
        [EnumMember(Value = "expired_on_capture")]
        ExpiredOnCapture,
        /// <summary>
        /// Истек срок оплаты: пользователь не подтвердил платеж за время, отведенное на оплату выбранным способом. Если пользователь еще хочет оплатить, вам необходимо повторить платеж с новым ключом идемпотентности, а пользователю — подтвердить его
        /// </summary>
        [EnumMember(Value = "expired_on_confirmation")]
        ExpiredOnConfirmation,
        /// <summary>
        /// Платеж заблокирован из-за подозрения в мошенничестве. При новой попытке оплаты пользователю следует использовать другое платежное средство
        /// </summary>
        [EnumMember(Value = "fraud_suspected")]
        FraudSuspected,
        /// <summary>
        /// Причина не детализирована. Пользователю следует обратиться к инициатору отмены платежа за уточнением подробностей
        /// </summary>
        [EnumMember(Value = "general_decline")]
        GeneralDecline,
        /// <summary>
        /// Превышены ограничения на платежи для кошелька в Яндекс.Деньгах. При новой попытке оплаты пользователю следует идентифицировать кошелек или выбрать другое платежное средство
        /// </summary>
        [EnumMember(Value = "identification_required")]
        IdentificationRequired,
        /// <summary>
        /// Не хватает денег для оплаты. Пользователю следует пополнить баланс или использовать другое платежное средство
        /// </summary>
        [EnumMember(Value = "insufficient_funds")]
        InsufficientFunds,
        /// <summary>
        /// Технические неполадки на стороне Яндекс.Кассы: не удалось обработать запрос в течение 30 секунд. Повторите платеж с новым ключом идемпотентности
        /// </summary>
        [EnumMember(Value = "internal_timeout")]
        InternalTimeout,
        /// <summary>
        /// Неправильно указан номер карты. При новой попытке оплаты пользователю следует ввести корректные данные
        /// </summary>
        [EnumMember(Value = "invalid_card_number")]
        InvalidCardNumber,
        /// <summary>
        /// Неправильно указан код CVV2 (CVC2, CID). При новой попытке оплаты пользователю следует ввести корректные данные
        /// </summary>
        [EnumMember(Value = "invalid_csc")]
        InvalidCsc,
        /// <summary>
        /// Организация, выпустившая платежное средство, недоступна. При новой попытке оплаты пользователю следует использовать другое платежное средство или повторить оплату позже
        /// </summary>
        [EnumMember(Value = "issuer_unavailable")]
        IssuerUnavailable,
        /// <summary>
        /// Исчерпан лимит платежей для данного платежного средства или вашего магазина. При новой попытке оплаты пользователю следует использовать другое платежное средство или повторить оплату на следующий день
        /// </summary>
        [EnumMember(Value = "payment_method_limit_exceeded")]
        PaymentMethodLimitExceeded,
        /// <summary>
        /// Запрещены операции данным платежным средством (например, карта заблокирована из-за утери, кошелек — из-за взлома мошенниками). Пользователю следует обратиться в организацию, выпустившую платежное средство
        /// </summary>
        [EnumMember(Value = "payment_method_restricted")]
        PaymentMethodRestricted,
        /// <summary>
        /// Нельзя провести безакцептное списание: пользователь отозвал разрешение на автоплатежи. Если пользователь еще хочет оплатить, вам необходимо создать новый платеж, а пользователю — подтвердить оплату
        /// </summary>
        [EnumMember(Value = "permission_revoked")]
        PermissionRevoked
    }
}
