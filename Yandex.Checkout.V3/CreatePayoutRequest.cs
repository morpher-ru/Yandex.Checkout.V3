﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
	public class CreatePayoutRequest
	{

		public Amount Amount { get; set; }

		public string Description { get; set; }

		public IDictionary<string, string> Metadata { get; set; }

		[JsonProperty("payout_token")]
		public string PayoutToken { get; set; }

		public PayoutDeal Deal { get; set; }
	}
}