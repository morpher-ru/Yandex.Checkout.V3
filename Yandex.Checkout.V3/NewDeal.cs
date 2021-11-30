using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
    public class NewDeal
    {
        public DealType Type { get; set; } = DealType.SafeDeal;

        public FeeMomentType FeeMoment { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> Metadata { get; set; }
    }
}
