namespace HotelService.Hotel.Application.DTOs.HotelDTOs;

public record HotelDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public int Stars { get; init; }
    public string PhoneNumber { get; init; } = default!;
    public string City { get; init; } = default!;
    public string Street { get; init; } = default!;
    public string District { get; init; } = default!;
    public bool HasPool { get; init; }
    public bool HasGym { get; init; }
    public bool HasSpa { get; init; }
    public bool HasRestaurant { get; init; }
    public bool HasBar { get; init; }
    public bool HasConferenceRoom { get; init; }
    public bool PetFriendly { get; init; }
    public string HotelType { get; init; } = default!;
    public TimeOnly CheckInTime { get; init; } = new(14, 0);
    public TimeOnly CheckOutTime { get; init; } = new(12, 0);
    public ICollection<string> Images { get; init; } = new List<string>();
    public double AverageRating { get; init; }
    public int TotalReviews { get; init; }
    public int TotalRooms { get; init; }
    public decimal? MinRoomPrice { get; init; }

}
