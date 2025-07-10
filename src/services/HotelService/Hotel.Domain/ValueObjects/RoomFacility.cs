namespace HotelService.Hotel.Domain.ValueObjects;

public record RoomFacility
{
    public bool HasWifi { get; init; }
    public bool HasAirConditioning { get; init; }
    public bool HasMinibar { get; init; }
    public bool HasBalcony { get; init; }
    public bool HasSeaView { get; init; }
    public bool HasCityView { get; init; }
    public bool NonSmoking { get; init; }

    public static RoomFacility Standard => new()
    {
        HasWifi = true,
        HasAirConditioning = true,
        NonSmoking = true,
        HasCityView = true
    };

    public static RoomFacility Deluxe => new()
    {
        HasWifi = true,
        HasAirConditioning = true,
        HasMinibar = true,
        HasBalcony = true,
        HasSeaView = true,
        NonSmoking = false
    };
}
