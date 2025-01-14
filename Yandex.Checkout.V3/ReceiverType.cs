namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Получатели оплаты.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public enum ReceiverType
    {
        /// <summary>
        /// Код получателя оплаты.
        /// </summary>
        BankAccount,

        /// <summary>
        /// Код получателя оплаты.
        /// </summary>
        MobileBalance,

        /// <summary>
        /// Электронный кошелек.
        /// </summary>
        DigitalWallet
    }
}
