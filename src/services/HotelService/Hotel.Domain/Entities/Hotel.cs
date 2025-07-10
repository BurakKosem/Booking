using System;
using HotelService.Hotel.Domain.Enums;
using HotelService.Hotel.Domain.ValueObjects;

namespace HotelService.Hotel.Domain.Entities;

public class Hotel : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Stars { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public Address Address { get; set; } = default!;
    public HotelFacility Facilities { get; set; } = default!;
    public HotelType HotelType { get; set; }
    public TimeOnly CheckInTime { get; set; } = new(14, 0);
    public TimeOnly CheckOutTime { get; set; } = new(12, 0);
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<string> Images { get; set; } = new List<string>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
