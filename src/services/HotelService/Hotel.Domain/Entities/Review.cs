using System;

namespace HotelService.Hotel.Domain.Entities;

public class Review : BaseEntity
{
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = default!;
    public Guid UserId { get; set; }
    public Guid ReservationId { get; set; }

    public int Rating { get; set; }
    public string Title { get; set; } = default!;
    public string Comment { get; set; } = default!;

}
