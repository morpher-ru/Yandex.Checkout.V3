namespace Yandex.Checkout.V3
{
    public class Card
    {
        public string First6 { get; init; }
        public string Last4 { get; init; }
        public string ExpiryYear { get; init; }
        public string ExpiryMonth { get; init; }
        public string CardType { get; init; }
        public string Csc { get; init; }
        public string CardHolder { get; init; }
        public string Number { get; init; }
        public string IssuerCountry { get; init; }
        public string IssuerName { get; init; }
    }
}
