namespace Reservation.Domain.ValueObjects;

public record ItemDetails
{
    public string ServiceName { get; init; } = default!;
    public Dictionary<string, string> Properties { get; init; } = new();

    private ItemDetails() { }

    public static ItemDetails Create(string serviceName, Dictionary<string, string> properties)
    {
        if (string.IsNullOrWhiteSpace(serviceName))
            throw new ArgumentException("Service name cannot be null or empty.", nameof(serviceName));

        return new ItemDetails
        {
            ServiceName = serviceName,
            Properties = properties ?? new Dictionary<string, string>()
        };
    }

    public static ItemDetails CreateForHotel(Dictionary<string, string>? additionalInfo = null)
    {
        return Create("HotelService", additionalInfo);
    }

    public static ItemDetails CreateForBus(Dictionary<string, string>? additionalInfo = null)
    {
        return Create("BusService", additionalInfo);
    }

    public static ItemDetails CreateForPlane(Dictionary<string, string>? additionalInfo = null)
    {
        return Create("PlaneService", additionalInfo);
    }
}
