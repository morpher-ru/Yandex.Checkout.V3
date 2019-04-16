using System.Runtime.Serialization;

namespace Yandex.Checkout.V3
{
    public enum VatDataType
    {
        [EnumMember(Value = "calculated")] Calculated,
        [EnumMember(Value = "untaxed")] Untaxed
    }
}
