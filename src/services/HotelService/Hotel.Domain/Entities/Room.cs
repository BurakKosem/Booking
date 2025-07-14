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
    public int Size { get; set; }
    public int BasePrice { get; set; }
    public int WeekendPriceMultiplier { get; set; } = 2;
    public int SeasonPriceMultiplier { get; set; } = 3;

    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = default!;
    public RoomFacility RoomFacilities { get; set; } = default!;
    public ICollection<string> Images { get; set; } = new List<string>();
}
