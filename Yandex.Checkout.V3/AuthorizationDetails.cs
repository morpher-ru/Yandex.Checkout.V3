namespace Yandex.Checkout.V3;

// ReSharper disable once ClassNeverInstantiated.Global
public class AuthorizationDetails
{
    /// <summary>
    /// Retrieval Reference Number, a unique identifier of a transaction in the issuer's system.
    /// Used for payments via bank card.
    /// </summary>
    public string Rrn { get; set; }
        
    /// <summary>
    /// The authorization code provided by the bank card issuer to confirm authorization.
    /// </summary>
    public string AuthCode { get; set; }
        
    /// <summary>
    /// 3‑D Secure authentication details provided by the user to confirm the payment.
    /// </summary>
    public ThreeDSecure ThreeDSecure { get; set; }
}