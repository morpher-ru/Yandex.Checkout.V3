namespace Yandex.Checkout.V3
{
    public class ReceiverMobileBalance : Receiver
    {
        public ReceiverMobileBalance() : base(ReceiverType.MobileBalance)
        {
        }

        /// <summary>
        /// Номер телефона для пополнения. Максимум 15 символов. Указывается в формате ITU-T E.164. 
        /// Пример: 79000000000.
        /// </summary>
        public string Phone { get; set; }
    }
}
