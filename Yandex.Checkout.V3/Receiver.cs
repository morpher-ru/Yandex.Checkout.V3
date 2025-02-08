namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Реквизиты получателя оплаты при пополнении электронного кошелька, банковского счета или баланса телефона.
    /// </summary>
    public abstract class Receiver
    {
        public string Type => GetType().Name.ToSnakeCase();
    }
}
