namespace HotelService.Hotel.Domain.ValueObjects;

public record HotelFacility
{
    public bool HasPool { get; init; }
    public bool HasGym { get; init; }
    public bool HasSpa { get; init; }
    public bool HasRestaurant { get; init; }
    public bool HasBar { get; init; }
    public bool HasConferenceRoom { get; init; }
    public bool PetFriendly { get; init; }

    public static HotelFacility BasicHotel => new()
    {
        HasPool = true,
        HasConferenceRoom = true,
    };

    public static HotelFacility LuxuryHotel => new()
    {
        HasPool = true,
        HasGym = true,
        HasSpa = true,
        HasRestaurant = true,
        HasBar = true,
        HasConferenceRoom = true,
    };

}
