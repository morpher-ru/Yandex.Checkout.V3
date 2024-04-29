// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Yandex.Checkout.V3;

public class Confirmation
{
    public ConfirmationType Type { get; set; }

    public string ReturnUrl { get; set; }

    public string ConfirmationUrl { get; set; }
        
    public string ConfirmationData { get; set; }

    public bool? Enforce { get; set; }

    public string Locale { get; set; }

    public string ConfirmationToken { get; set; }
}