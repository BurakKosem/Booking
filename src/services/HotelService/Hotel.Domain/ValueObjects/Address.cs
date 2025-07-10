namespace HotelService.Hotel.Domain.ValueObjects;

public record Address
{
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string District { get; set; } = default!;

    public Address(string street, string city, string district)
    {
        Street = street;
        City = city;
        District = district;
    }

    public string FullAddress => $"{Street}, {District}, {City}";
}
