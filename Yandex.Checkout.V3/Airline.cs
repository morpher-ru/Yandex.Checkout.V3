namespace Yandex.Checkout.V3;

/// <summary>
/// Авиалинии
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class Airline
{
    public string BookingReference { get; init; }
    public string TicketNumber { get; init; }
    public List<Passenger> Passengers { get; init; } = new();
    public List<Leg> Legs { get; init; } = new();
}