namespace HotelService.Hotel.Application.DTOs.HotelDTOs;

public record UpdateHotelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Stars { get; set; }
    public string? PhoneNumber { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;

    public bool HasPool { get; set; }
    public bool HasGym { get; set; }
    public bool HasSpa { get; set; }
    public bool HasRestaurant { get; set; }
    public bool HasBar { get; set; }
    public bool HasConferenceRoom { get; set; }
    public bool PetFriendly { get; set; }

    public string HotelType { get; set; } = string.Empty;
    public TimeOnly? CheckInTime { get; set; }
    public TimeOnly? CheckOutTime { get; set; }
    public List<string> Images { get; set; } = new();
}
