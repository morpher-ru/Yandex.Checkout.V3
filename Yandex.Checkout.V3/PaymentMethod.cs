using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    public class PaymentMethod
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentMethodType Type { get; set; }

        public string Id { get; set; }
        public bool Saved { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string PaymentPurpose { get; set; }
        public string AccountNumber { get; set; }
        public string PaymentMethodToken { get; set; }
        public string GoogleTransactionId { get; set; }
        public string PaymentData { get; set; }
        public Card Card { get; set; }
        public VatData VatData { get; set; }

        #region Helpers

        public static PaymentMethod Alfabank(string login) => new PaymentMethod()
        {
            Type = PaymentMethodType.Alfabank,
            Login = login
        };

        public static PaymentMethod ApplePay(string paymentData) => new PaymentMethod()
        {
            Type = PaymentMethodType.ApplePay,
            PaymentData = paymentData
        };

        public static PaymentMethod B2BSberbank(string paymentPurpose, VatData vatData) => new PaymentMethod()
        {
            Type = PaymentMethodType.B2BSberbank,
            PaymentPurpose = paymentPurpose,
            VatData = vatData
        };

        public static PaymentMethod BankCard(Card card) => new PaymentMethod()
        {
            Type = PaymentMethodType.BankCard,
            Card = card
        };

        public static PaymentMethod Cash(string phone) => new PaymentMethod()
        {
            Type = PaymentMethodType.Cash,
            Phone = phone
        };

        public static PaymentMethod GooglePay(string paymentMethodToken, string googleTransactionId) => new PaymentMethod()
        {
            Type = PaymentMethodType.GooglePay,
            PaymentMethodToken = paymentMethodToken,
            GoogleTransactionId = googleTransactionId
        };

        public static PaymentMethod Installments() => new PaymentMethod() {Type = PaymentMethodType.Installments};

        public static PaymentMethod MobileBalance(string phone) => new PaymentMethod()
        {
            Type = PaymentMethodType.MobileBalance,
            Phone = phone
        };

        public static PaymentMethod Qiwi(string phone) => new PaymentMethod()
        {
            Type = PaymentMethodType.Qiwi,
            Phone = phone
        };

        public static PaymentMethod Sberbank(string phone) => new PaymentMethod()
        {
            Type = PaymentMethodType.Sberbank,
            Phone = phone
        };

        public static PaymentMethod Webmoney() => new PaymentMethod()
        {
            Type = PaymentMethodType.Webmoney,
        };

        public static PaymentMethod YandexMoney() => new PaymentMethod()
        {
            Type = PaymentMethodType.YandexMoney,
        };

        #endregion
    }
}
