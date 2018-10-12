using System.Runtime.Serialization;

namespace Yandex.Checkout.V3
{
    public enum ConfirmationType
    {
        [EnumMember(Value = "redirect")]
        Redirect,
        [EnumMember(Value = "external")]
        External
    }
}
