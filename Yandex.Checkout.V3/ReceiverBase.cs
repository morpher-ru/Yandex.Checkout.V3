namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Реквизиты получателя оплаты при пополнении электронного кошелька, банковского счета или баланса телефона.
    /// </summary>
    public class ReceiverBase
    {
        public ReceiverBase(ReceiverType type)
        {
            Type = type;
        }

        public ReceiverType Type { get; }
    }
}
