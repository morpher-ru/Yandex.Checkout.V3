namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Платежное поручение — распоряжение на перевод банку для оплаты 
    /// жилищно-коммунальных услуг (ЖКУ), сведения о платеже для регистрации в ГИС ЖКХ.
    /// Необходимо передавать при оплате ЖКУ.
    /// <see href="https://yookassa.ru/developers/payment-acceptance/scenario-extensions/utility-payments"/>
    /// </summary>
    public class PaymentOrder
    {
        /// <summary>
        /// Код вида платежного поручения.
        /// Значение по умолчанию - utilities.
        /// </summary>
        /// <remarks>
        /// Обязательный параметр.
        /// </remarks>
        public PaymentOrderType Type { get; set; } = PaymentOrderType.Utilities;

        /// <summary>
        /// Номер лицевого счета на стороне поставщика ЖКУ. 
        /// </summary>
        /// <remarks>
        /// Обязательный параметр, если не передан 
        /// payment_document_id, payment_document_number, unified_account_number или service_id.
        /// </remarks>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Сумма платежного поручения — сумма, 
        /// которую пользователь переводит получателю платежа. 
        /// Равна общей сумме платежа.
        /// </summary>
        /// <remarks>
        /// Обязательный параметр.
        /// </remarks>
        public Amount Amount { get; set; }

        /// <summary>
        /// Код бюджетной классификации (КБК).
        /// </summary>
        public string Kbk { get; set; }

        /// <summary>
        /// Код ОКТМО (Общероссийский классификатор территорий муниципальных образований).
        /// </summary>
        public string Oktmo { get; set; }

        /// <summary>
        /// Идентификатор платежного документа. 
        /// </summary>
        /// <remarks>
        /// Обязательный параметр, если не передан 
        /// payment_document_number, account_number, unified_account_number или service_id.
        /// </remarks>
        public string PaymentDocumentId { get; set; }

        /// <summary>
        /// Номер платежного документа на стороне поставщика ЖКУ. 
        /// </summary>
        /// <remarks>
        /// Обязательный параметр, если не передан 
        /// payment_document_id, account_number, unified_account_number или service_id.
        /// </remarks>
        public string PaymentDocumentNumber { get; set; }

        /// <summary>
        /// Период оплаты, за который выставлены начисления и за который вносится оплата.
        /// </summary>
        public PaymentPeriod PaymentPeriod { get; set; }

        /// <summary>
        /// Назначение платежа (не больше 210 символов). 
        /// <para>Обязательный параметр.</para>
        /// </summary>
        /// <remarks>
        /// Рекомендуется формировать с учетом рекомендаций из Письма Банка России № ИН-04-45|12 от 22.02.2018. 
        /// Пример: Оплата ЖКХ;ЕЛС 80KX478547;ПРД 12.2024;Иванов Иван;г.Москва, ул.Флотская, д.1, кв.1
        /// </remarks>
        public string PaymentPurpose { get; set; }

        /// <summary>
        /// Получатель платежа — государственная или коммерческая организация, 
        /// которая предоставляет услуги или является информационным посредником, 
        /// который собирает и обрабатывает начисления от других поставщиков услуг.
        /// </summary>
        /// <remarks>
        /// Обязательный параметр.
        /// </remarks>
        public Payee Recipient { get; set; }

        /// <summary>
        /// Идентификатор жилищно-коммунальной услуги (ЖКУ).
        /// </summary>
        /// <remarks>
        /// Обязательный параметр, если не передан 
        /// payment_document_number, account_number, unified_account_number или service_id.
        /// </remarks>
        public string ServiceId { get; set; }

        /// <summary>
        /// Единый лицевой счет. Уникальный идентификатор в ГИС ЖКХ, 
        /// который характеризует связку «собственник-помещение».
        /// </summary>
        /// <remarks>
        /// Обязательный параметр, если не передан 
        /// payment_document_number, account_number, unified_account_number или service_id.
        /// </remarks>
        public string UnifiedAccountNumber { get; set; }
    }
}
