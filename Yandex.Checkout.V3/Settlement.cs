namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Совершенный расчёт
    /// </summary>
    public class Settlement
    {
        /// <summary>
        /// Тип расчета
        /// </summary>
        public SettlementType Type { get; set; }

        /// <summary>
        /// Сумма расчета
        /// </summary>
        public Amount Amount { get; set; }
    }
}
