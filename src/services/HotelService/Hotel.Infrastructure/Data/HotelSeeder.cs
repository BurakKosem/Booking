using HotelService.Hotel.Domain.Entities;
using HotelService.Hotel.Domain.Enums;
using HotelService.Hotel.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HotelService.Hotel.Infrastructure.Data;

public static class HotelSeeder
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

        try
        {
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            await SeedAsync(context);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static async Task SeedAsync(HotelDbContext context)
    {
        if (await context.Hotels.AnyAsync())
        {
            return; 
        }

        var hotels = CreateSampleHotels();

        context.Hotels.AddRange(hotels);
        await context.SaveChangesAsync();
    }

    private static List<HotelService.Hotel.Domain.Entities.Hotel> CreateSampleHotels()
    {
        return new List<HotelService.Hotel.Domain.Entities.Hotel>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Grand Luxury Hotel",
                Description = "5-star luxury hotel in the heart of Istanbul with panoramic Bosphorus views",
                Stars = 5,
                PhoneNumber = "+90 212 555 0001",
                HotelType = HotelType.Luxury,
                Address = new Address("Nişantaşı Mahallesi", "Istanbul", "Şişli"),
                Facilities = HotelFacility.LuxuryHotel,
                CheckInTime = new TimeOnly(15, 0),
                CheckOutTime = new TimeOnly(12, 0),
                Images = new List<string>
                {
                    "https://example.com/hotel1/lobby.jpg",
                    "https://example.com/hotel1/exterior.jpg",
                    "https://example.com/hotel1/pool.jpg"
                },
                CreatedDate = DateTime.UtcNow,
                Rooms = CreateSampleRooms(1)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Boutique Central Hotel",
                Description = "Cozy boutique hotel in Beyoğlu with authentic Turkish atmosphere",
                Stars = 4,
                PhoneNumber = "+90 212 555 0002",
                HotelType = HotelType.Boutique,
                Address = new Address("Galata Mahallesi", "Istanbul", "Beyoğlu"),
                Facilities = HotelFacility.BasicHotel,
                CheckInTime = new TimeOnly(14, 0),
                CheckOutTime = new TimeOnly(11, 0),
                Images = new List<string>
                {
                    "https://example.com/hotel2/lobby.jpg",
                    "https://example.com/hotel2/room.jpg"
                },
                CreatedDate = DateTime.UtcNow,
                Rooms = CreateSampleRooms(2)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Seaside Resort Hotel",
                Description = "Beautiful resort hotel on the Aegean coast with private beach",
                Stars = 5,
                PhoneNumber = "+90 232 555 0003",
                HotelType = HotelType.Resort,
                Address = new Address("Çeşme Marina", "Izmir", "Çeşme"),
                Facilities = new HotelFacility
                {
                    HasPool = true,
                    HasGym = true,
                    HasSpa = true,
                    HasRestaurant = true,
                    HasBar = true,
                    HasConferenceRoom = false,
                    PetFriendly = true,
                },
                CheckInTime = new TimeOnly(16, 0),
                CheckOutTime = new TimeOnly(12, 0),
                Images = new List<string>
                {
                    "https://example.com/hotel3/beach.jpg",
                    "https://example.com/hotel3/pool.jpg",
                    "https://example.com/hotel3/spa.jpg"
                },
                CreatedDate = DateTime.UtcNow,
                Rooms = CreateSampleRooms(3)
            }
        };
    }

    private static List<Room> CreateSampleRooms(int hotelSeed)
    {
        var rooms = new List<Room>();
        var basePrice = hotelSeed switch
        {
            1 => 450m, 
            2 => 280m, 
            3 => 380m, 
            _ => 300m
        };

        for (int i = 1; i <= 10; i++)
        {
            rooms.Add(new Room
            {
                Id = Guid.NewGuid(),
                Name = $"Standard Room {i:D3}",
                Description = "Comfortable standard room with city view",
                TotalRoomCount = (hotelSeed * 5) + i,
                RoomType = RoomType.Double,
                MaxOccupancy = 2,
                Size = 25m,
                BasePrice = basePrice,
                WeekendPriceMultiplier = 1.2m,
                SeasonPriceMultiplier = 1.4m,
                RoomFacilities = RoomFacility.Standard,
                Images = new List<string>
                {
                    $"https://example.com/hotel{hotelSeed}/room{i}/1.jpg",
                    $"https://example.com/hotel{hotelSeed}/room{i}/2.jpg"
                },
                CreatedDate = DateTime.UtcNow
            });
        }

        for (int i = 11; i <= 15; i++)
        {
            rooms.Add(new Room
            {
                Id = Guid.NewGuid(),
                Name = $"Deluxe Room {i:D3}",
                Description = "Spacious deluxe room with premium amenities",
                TotalRoomCount = (hotelSeed * 10) + i,
                RoomType = RoomType.King,
                MaxOccupancy = 2,
                Size = 35m,
                BasePrice = basePrice * 1.5m,
                WeekendPriceMultiplier = 1.2m,
                SeasonPriceMultiplier = 1.4m,
                RoomFacilities = RoomFacility.Deluxe,
                Images = new List<string>
                {
                    $"https://example.com/hotel{hotelSeed}/deluxe{i}/1.jpg",
                    $"https://example.com/hotel{hotelSeed}/deluxe{i}/2.jpg",
                    $"https://example.com/hotel{hotelSeed}/deluxe{i}/3.jpg"
                },
                CreatedDate = DateTime.UtcNow
            });
        }

        for (int i = 16; i <= 20; i++)
        {
            rooms.Add(new Room
            {
                Id = Guid.NewGuid(),
                Name = $"Single Room {i:D3}",
                Description = "Cozy single room perfect for solo travelers",
                TotalRoomCount = (hotelSeed * 15) + i,
                RoomType = RoomType.Single,
                MaxOccupancy = 1,
                Size = 18m,
                BasePrice = basePrice * 0.7m,
                WeekendPriceMultiplier = 1.1m,
                SeasonPriceMultiplier = 1.3m,
                RoomFacilities = new RoomFacility
                {
                    HasWifi = true,
                    HasAirConditioning = true,
                    HasMinibar = false,
                    HasBalcony = false,
                    HasSeaView = false,
                    HasCityView = true,
                    NonSmoking = true
                },
                Images = new List<string>
                {
                    $"https://example.com/hotel{hotelSeed}/single{i}/1.jpg"
                },
                CreatedDate = DateTime.UtcNow
            });
        }

        return rooms;
    }
}