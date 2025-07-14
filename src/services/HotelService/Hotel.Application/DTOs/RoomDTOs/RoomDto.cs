namespace Hotel.Application.DTOs.RoomDTOs;

public record RoomDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public int TotalRoomCount { get; init; }
    public string RoomType { get; init; } = default!;
    public int MaxOccupancy { get; init; }
    public int Size { get; init; }

    public int BasePrice { get; init; }
    public int WeekendPriceMultiplier { get; init; }
    public int SeasonPriceMultiplier { get; init; }

    public bool HasWifi { get; init; }
    public bool HasAirConditioning { get; init; }
    public bool HasMinibar { get; init; }
    public bool HasBalcony { get; init; }
    public bool HasSeaView { get; init; }
    public bool HasCityView { get; init; }
    public bool NonSmoking { get; init; }
    public ICollection<string> Images { get; init; } = default!;

}
