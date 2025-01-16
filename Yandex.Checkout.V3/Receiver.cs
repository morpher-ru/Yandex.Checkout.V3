namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Реквизиты получателя оплаты при пополнении электронного кошелька, банковского счета или баланса телефона.
    /// </summary>
    public abstract class Receiver
    {
        public Receiver(ReceiverType type)
        {
            Type = type;
        }

        public ReceiverType Type { get; }
    }
}
