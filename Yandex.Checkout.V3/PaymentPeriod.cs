namespace Yandex.Checkout.V3
{
    public class PaymentPeriod
    {
        /// <summary>
        /// Месяц периода. Например, 1 — январь.
        /// </summary>
        /// <remarks>
        /// Обязательный параметр.
        /// </remarks>
        public int Month { get; set; }

        /// <summary>
        /// Год периода. Например, 2025.
        /// </summary>
        /// <remarks>
        /// Обязательный параметр.
        /// </remarks>
        public int Year { get; set; }
    }
}
