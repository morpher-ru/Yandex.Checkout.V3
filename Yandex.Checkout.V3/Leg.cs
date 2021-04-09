using System;

namespace Yandex.Checkout.V3
{
    public class Leg
    {
        public string DepartureAirport { get; init; }
        public string DestinationAirport { get; init; }
        public DateTime DepartureDate { get; init; }
    }
}
