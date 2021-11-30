using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
    public class PaymentDeal
    {
        /// <summary>
        /// Id сделки, в рамках которой совершается платеж
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Перечень совершенных расчетов
        /// </summary>
        public List<Settlement> Settlements { get; set; } = new();
    }
}
