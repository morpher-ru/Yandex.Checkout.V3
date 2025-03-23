// ReSharper disable UnusedMember.Global

namespace Yandex.Checkout.V3;

/// <summary>
/// Коды НДС
/// </summary>
public enum VatCode
{
    /// <summary>
    /// Без НДС
    /// </summary>
    NoVat = 1,

    /// <summary>
    /// НДС по ставке 0%
    /// </summary>
    Vat0 = 2,

    /// <summary>
    /// НДС по ставке 10%
    /// </summary>
    Vat10 = 3,

    /// <summary>
    /// НДС по ставке 20%
    /// </summary>
    Vat20 = 4,

    /// <summary>
    /// НДС по расчетной ставке 10/110
    /// </summary>
    Vat110 = 5,

    /// <summary>
    /// НДС чека по расчетной ставке 20/120
    /// </summary>
    Vat120 = 6,

    /// <summary>
    /// НДС по ставке 5%
    /// </summary>
    Vat5 = 7,

    /// <summary>
    /// НДС по ставке 7%
    /// </summary>
    Vat7 = 8,

    /// <summary>
    /// НДС по расчетной ставке 5/105
    /// </summary>
    Vat105 = 9,

    /// <summary>
    /// НДС по расчетной ставке 7/107
    /// </summary>
    Vat107 = 10
}
