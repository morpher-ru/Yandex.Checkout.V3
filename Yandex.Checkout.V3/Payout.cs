using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
	public class Payout
	{
		public string Id { get; set; }

		public Amount Amount { get; set; }

		public PayoutStatus Status { get; set; }

		[JsonProperty("payout_destination")]
		public PayoutDestination PayoutDestination { get; set; }

		public string Description { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		public PayoutDeal Deal { get; set; }

		public IDictionary<string, string> Metadata { get; set; }

		[JsonProperty("cancellation_details")]
		public CancellationDetails CancellationDetails { get; set; }

		public bool Test { get; set; }
	}
}