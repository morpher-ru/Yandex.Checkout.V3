using System.Runtime.Serialization;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Типы платежей
    /// </summary>
    public enum PaymentMethodType
    {
        [EnumMember(Value = "sberbank")]
        Sberbank,
        [EnumMember(Value = "bank_card")]
        BankCard,
        [EnumMember(Value = "cash")]
        Cash,
        [EnumMember(Value = "yandex_money")]
        YandexMoney,
        [EnumMember(Value = "qiwi")]
        Qiwi,
        [EnumMember(Value = "alfabank")]
        Alfabank,
        [EnumMember(Value = "webmoney")]
        Webmoney,
        [EnumMember(Value = "apple_pay")]
        ApplePay,
        [EnumMember(Value = "mobile_balance")]
        MobileBalance,
        [EnumMember(Value = "installments")]
        Installments,
        [EnumMember(Value = "psb")]
        Psb,
        [EnumMember(Value = "google_pay")]
        GooglePay,
        [EnumMember(Value = "b2b_sberbank")]
        B2BSberbank,
    }
}
