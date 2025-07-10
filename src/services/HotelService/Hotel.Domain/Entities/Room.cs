using System;
using HotelService.Hotel.Domain.Enums;
using HotelService.Hotel.Domain.ValueObjects;

namespace HotelService.Hotel.Domain.Entities;

public class Room : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int TotalRoomCount { get; set; }
    public RoomType RoomType { get; set; }
    public int MaxOccupancy { get; set; }
    public decimal Size { get; set; }
    public decimal BasePrice { get; set; }
    public decimal WeekendPriceMultiplier { get; set; } = 1.1m;
    public decimal SeasonPriceMultiplier { get; set; } = 1.5m;

    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = default!;
    public RoomFacility RoomFacilities { get; set; } = default!;
    public ICollection<string> Images { get; set; } = new List<string>();
}
