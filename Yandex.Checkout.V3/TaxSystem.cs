namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Коды систем налогообложения
    /// </summary>
    public enum TaxSystem
    {
        /// <summary>
        /// Общая система налогообложения
        /// </summary>
        General = 1,

        /// <summary>
        /// Упрощенная (УСН, доходы)
        /// </summary>
        Simplified = 2,

        /// <summary>
        /// Упрощенная (УСН, доходы минус расходы)
        /// </summary>
        SimplifiedWithExpenses = 3,

        /// <summary>
        /// Единый налог на вмененный доход (ЕНВД)
        /// </summary>
        Imputed = 4,

        /// <summary>
        /// Единый сельскохозяйственный налог (ЕСН)
        /// </summary>
        Agricultural = 5,

        /// <summary>
        /// Патентная система налогообложения
        /// </summary>
        Patent = 6
    }
}
