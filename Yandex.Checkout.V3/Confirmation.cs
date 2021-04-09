namespace Yandex.Checkout.V3
{
    public class Confirmation
    {
        public ConfirmationType Type { get; init; }

        public string ReturnUrl { get; init; }

        public string ConfirmationUrl { get; init; }

        public bool? Enforce { get; init; }

        public string Locale { get; init; }

        public string ConfirmationToken { get; init; }
    }
}
