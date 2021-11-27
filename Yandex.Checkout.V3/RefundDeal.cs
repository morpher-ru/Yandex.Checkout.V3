using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
    public class RefundDeal
    {
        /// <summary>
        /// Id сделки.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Перечень совершенных расчетов.
        /// </summary>
        [JsonProperty("refund_settlements")]
        public List<Settlement> Settlements { get; set; } = new();
    }
}
