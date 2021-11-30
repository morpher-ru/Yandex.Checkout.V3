using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
	public class NewPayout
	{
		public Amount Amount { get; set; }

		public string Description { get; set; }

		public IDictionary<string, string> Metadata { get; set; }

		public string PayoutToken { get; set; }

		public PayoutDeal Deal { get; set; }
	}
}
