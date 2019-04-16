namespace Yandex.Checkout.V3
{
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
        /// НДС по ставке 18%
        /// </summary>
        Vat18 = 4,

        /// <summary>
        /// НДС по расчетной ставке 10/110
        /// </summary>
        Vat110 = 5,

        /// <summary>
        /// НДС чека по расчетной ставке 18/118
        /// </summary>
        Vat118 = 6
    }
}
