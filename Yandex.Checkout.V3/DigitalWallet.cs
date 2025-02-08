namespace Yandex.Checkout.V3
{
    public class DigitalWallet : Receiver
    {
        /// <summary>
        /// Идентификатор электронного кошелька для пополнения. Максимум 20 символов.
        /// </summary>
        public string AccountNumber { get; set; }
    }
}
