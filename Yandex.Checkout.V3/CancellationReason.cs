namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Причина отмены платежа
    /// </summary>
    public static class CancellationReason
    {
        /// <summary>
        /// Не пройдена аутентификация по 3-D Secure. При новой попытке оплаты пользователю следует пройти аутентификацию, использовать другое платежное средство или обратиться в банк за уточнениями
        /// </summary>
        public const string ThreeDSecureFailed = "3d_secure_failed";
        /// <summary>
        /// Оплата данным платежным средством отклонена по неизвестным причинам. Пользователю следует обратиться в организацию, выпустившую платежное средство
        /// </summary>
        public const string CallIssuer = "call_issuer";
        /// <summary>
        /// Платеж отменен по API при оплате в две стадии
        /// </summary>
        public const string CanceledByMerchant = "canceled_by_merchant";
        /// <summary>
        /// Истек срок действия банковской карты. При новой попытке оплаты пользователю следует использовать другое платежное средство
        /// </summary>
        public const string CardExpired = "card_expired";
        /// <summary>
        /// Нельзя заплатить банковской картой, выпущенной в этой стране. При новой попытке оплаты пользователю следует использовать другое платежное средство.
        /// </summary>
        public const string CountryForbidden = "country_forbidden";
        /// <summary>
        /// Истек срок списания оплаты у двухстадийного платежа. Если вы еще хотите принять оплату, повторите платеж с новым ключом идемпотентности и спишите деньги после подтверждения платежа пользователем
        /// </summary>
        public const string ExpiredOnCapture = "expired_on_capture";
        /// <summary>
        /// Истек срок оплаты: пользователь не подтвердил платеж за время, отведенное на оплату выбранным способом. Если пользователь еще хочет оплатить, вам необходимо повторить платеж с новым ключом идемпотентности, а пользователю — подтвердить его
        /// </summary>
        public const string ExpiredOnConfirmation = "expired_on_confirmation";
        /// <summary>
        /// Платеж заблокирован из-за подозрения в мошенничестве. При новой попытке оплаты пользователю следует использовать другое платежное средство
        /// </summary>
        public const string FraudSuspected = "fraud_suspected";
        /// <summary>
        /// Причина не детализирована. Пользователю следует обратиться к инициатору отмены платежа за уточнением подробностей
        /// </summary>
        public const string GeneralDecline = "general_decline";
        /// <summary>
        /// Превышены ограничения на платежи для кошелька в Яндекс.Деньгах. При новой попытке оплаты пользователю следует идентифицировать кошелек или выбрать другое платежное средство
        /// </summary>
        public const string IdentificationRequired = "identification_required";
        /// <summary>
        /// Не хватает денег для оплаты. Пользователю следует пополнить баланс или использовать другое платежное средство
        /// </summary>
        public const string InsufficientFunds = "insufficient_funds";
        /// <summary>
        /// Технические неполадки на стороне Яндекс.Кассы: не удалось обработать запрос в течение 30 секунд. Повторите платеж с новым ключом идемпотентности
        /// </summary>
        public const string InternalTimeout = "internal_timeout";
        /// <summary>
        /// Неправильно указан номер карты. При новой попытке оплаты пользователю следует ввести корректные данные
        /// </summary>
        public const string InvalidCardNumber = "invalid_card_number";
        /// <summary>
        /// Неправильно указан код CVV2 (CVC2, CID). При новой попытке оплаты пользователю следует ввести корректные данные
        /// </summary>
        public const string InvalidCsc = "invalid_csc";
        /// <summary>
        /// Организация, выпустившая платежное средство, недоступна. При новой попытке оплаты пользователю следует использовать другое платежное средство или повторить оплату позже
        /// </summary>
        public const string IssuerUnavailable = "issuer_unavailable";
        /// <summary>
        /// Исчерпан лимит платежей для данного платежного средства или вашего магазина. При новой попытке оплаты пользователю следует использовать другое платежное средство или повторить оплату на следующий день
        /// </summary>
        public const string PaymentMethodLimitExceeded = "payment_method_limit_exceeded";
        /// <summary>
        /// Запрещены операции данным платежным средством (например, карта заблокирована из-за утери, кошелек — из-за взлома мошенниками). Пользователю следует обратиться в организацию, выпустившую платежное средство
        /// </summary>
        public const string PaymentMethodRestricted = "payment_method_restricted";
        /// <summary>
        /// Нельзя провести безакцептное списание: пользователь отозвал разрешение на автоплатежи. Если пользователь еще хочет оплатить, вам необходимо создать новый платеж, а пользователю — подтвердить оплату
        /// </summary>
         public const string PermissionRevoked = "permission_revoked";
    }
}
