namespace Hotel.Application.DTOs.RoomDTOs;

public record CreateRoomDto
{
    public string Name { get; init; } 
    public string Description { get; init; } 
    public int TotalRoomCount { get; init; }
    public string RoomType { get; init; }  
    public int MaxOccupancy { get; init; }
    public decimal Size { get; init; }
    public decimal BasePrice { get; init; }
    public decimal WeekendPriceMultiplier { get; init; } 
    public decimal SeasonPriceMultiplier { get; init; } 

    public bool HasWifi { get; init; }
    public bool HasAirConditioning { get; init; }
    public bool HasMinibar { get; init; }
    public bool HasBalcony { get; init; }
    public bool HasSeaView { get; init; }
    public bool HasCityView { get; init; }
    public bool NonSmoking { get; init; }

    public ICollection<string> Images { get; init; } = new List<string>();

    public Guid HotelId { get; init; }
}
