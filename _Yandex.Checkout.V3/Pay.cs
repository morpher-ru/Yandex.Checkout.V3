namespace Yandex.Checkout.V3
{
    class Pay
    {
        public bool capture { get; set; }
        public Amount amount = new Amount();
        public Confirmation_Return confirmation = new Confirmation_Return();
    }
}
