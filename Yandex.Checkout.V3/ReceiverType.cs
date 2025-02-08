namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Получатели оплаты.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    internal enum ReceiverType
    {
        BankAccount,
        MobileBalance,
        DigitalWallet
    }
}
