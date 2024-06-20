namespace Yandex.Checkout.V3;

// ReSharper disable once ClassNeverInstantiated.Global
public class VatData
{
    public VatDataType Type { get; set; }
    public string Rate { get; set; }
    public Amount Amount { get; set; }
    public PayerBankDetails PayerBankDetails { get; set; }
}