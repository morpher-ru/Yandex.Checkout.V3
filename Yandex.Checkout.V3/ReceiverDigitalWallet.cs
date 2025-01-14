namespace Yandex.Checkout.V3
{
    public class ReceiverDigitalWallet : ReceiverBase
    {
        public ReceiverDigitalWallet() : base(ReceiverType.DigitalWallet)
        {
        }

        /// <summary>
        /// Идентификатор электронного кошелька для пополнения. Максимум 20 символов.
        /// </summary>
        public string AccountNumber { get; set; }
    }
}
