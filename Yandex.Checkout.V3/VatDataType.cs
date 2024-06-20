

// ReSharper disable UnusedMember.Global

namespace Yandex.Checkout.V3;

[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
public enum VatDataType
{
    Calculated,
    Untaxed
}
