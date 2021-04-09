namespace Yandex.Checkout.V3
{
    public class VatData
    {
        public VatDataType Type { get; init; }
        public string Rate { get; init; }
        public Amount Amount { get; init; }
        public PayerBankDetails PayerBankDetails { get; init; }
    }
}
