namespace Yandex.Checkout.V3
{
    public class Card
    {
        public string First6 { get; set; }
        public string Last4 { get; set; }
        public string ExpiryYear { get; set; }
        public string ExpiryMonth { get; set; }
        public string CardType { get; set; }
        public string Csc { get; set; }
        public string CardHolder { get; set; }
        public string Number { get; set; }
        public string IssuerCountry { get; set; }
        public string IssuerName { get; set; }
    }
}
