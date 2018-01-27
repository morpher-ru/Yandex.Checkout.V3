namespace Yandex.Checkout.V3
{
    class Pay_Result
    {
        public string id { get; set; }
        public string status { get; set; }
        public string paid { get; set; }
        public string created_at { get; set; }

        public Amount amount = new Amount();
        public Confirmation_Result confirmation = new Confirmation_Result();
        public Recipient recipient = new Recipient();
    }
}
