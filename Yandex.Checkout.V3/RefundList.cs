using System.Collections.Generic;
using System.Linq;

namespace Yandex.Checkout.V3
{
    public class RefundList
    {
        [JsonProperty("type")]
        public string ResponseKind { get; set; }

        [JsonProperty("items")]
        public List<Refund> Refunds { get; set; }

        [JsonProperty("next_cursor")]
        public string NextCursor { get; set; }
    }
}
