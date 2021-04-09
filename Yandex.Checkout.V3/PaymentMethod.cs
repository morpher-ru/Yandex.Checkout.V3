// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Yandex.Checkout.V3
{
    public class PaymentMethod
    {
        public string Type { get; set; }

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

        public static PaymentMethod Alfabank(string login) => new()
        {
            Type = PaymentMethodType.Alfabank,
            Login = login
        };

        public static PaymentMethod ApplePay(string paymentData) => new()
        {
            Type = PaymentMethodType.ApplePay,
            PaymentData = paymentData
        };

        public static PaymentMethod B2BSberbank(string paymentPurpose, VatData vatData) => new()
        {
            Type = PaymentMethodType.B2BSberbank,
            PaymentPurpose = paymentPurpose,
            VatData = vatData
        };

        public static PaymentMethod BankCard(Card card) => new()
        {
            Type = PaymentMethodType.BankCard,
            Card = card
        };

        public static PaymentMethod Cash(string phone) => new()
        {
            Type = PaymentMethodType.Cash,
            Phone = phone
        };

        public static PaymentMethod GooglePay(string paymentMethodToken, string googleTransactionId) => new()
        {
            Type = PaymentMethodType.GooglePay,
            PaymentMethodToken = paymentMethodToken,
            GoogleTransactionId = googleTransactionId
        };

        public static PaymentMethod Installments() => new() {Type = PaymentMethodType.Installments};

        public static PaymentMethod MobileBalance(string phone) => new()
        {
            Type = PaymentMethodType.MobileBalance,
            Phone = phone
        };

        public static PaymentMethod Qiwi(string phone) => new()
        {
            Type = PaymentMethodType.Qiwi,
            Phone = phone
        };

        public static PaymentMethod Sberbank(string phone) => new()
        {
            Type = PaymentMethodType.Sberbank,
            Phone = phone
        };

        public static PaymentMethod Webmoney() => new()
        {
            Type = PaymentMethodType.Webmoney,
        };

        public static PaymentMethod YooMoney() => new()
        {
            Type = PaymentMethodType.YooMoney
        };

        public static PaymentMethod WeChat() => new()
        {
            Type = PaymentMethodType.WeChat,
        };

        public static PaymentMethod Tinkoff() => new()
        {
            Type = PaymentMethodType.Tinkoff,
        };

        #endregion
    }
}
