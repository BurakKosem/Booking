using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bus.Domain.Entities;
using Bus.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bus.Infrastructure.Data;

public class DataSeed
{
    public static async Task SeedAsync(BusDbContext context)
    {
        if (await context.BusTrips.AnyAsync())
            return;

        var companies = new List<BusCompany>
        {
            new() { Name = "Örnek Turizm", PhoneNumber = "02120000001", Email = "info@ornekturizm.com" },
            new() { Name = "Hızlı Ekspres", PhoneNumber = "02120000002", Email = "info@hizliekspres.com" },
            new() { Name = "Mavi Yol", PhoneNumber = "02120000003", Email = "info@maviyol.com" }
        };

        var routes = new List<Route>
        {
            new() { Origin = "İstanbul", Destination = "Ankara" },
            new() { Origin = "Ankara", Destination = "İzmir" },
            new() { Origin = "İzmir", Destination = "Antalya" },
            new() { Origin = "Bursa", Destination = "İstanbul" },
            new() { Origin = "Adana", Destination = "Gaziantep" },
            new() { Origin = "Trabzon", Destination = "Samsun" },
            new() { Origin = "Konya", Destination = "Kayseri" }
        };

        var trips = new List<BusTrip>();
        var random = new Random();

        for (int i = 0; i < 20; i++)
        {
            var company = companies[random.Next(companies.Count)];
            var route = routes[random.Next(routes.Count)];
            var departure = DateTime.Today.AddDays(random.Next(1, 15)).AddHours(random.Next(6, 23));
            var duration = TimeSpan.FromHours(random.Next(4, 10));
            var arrival = departure.Add(duration);

            trips.Add(new BusTrip
            {
                BusCompany = company,
                Route = new Route { Origin = route.Origin, Destination = route.Destination },
                DepartureTime = departure,
                ArrivalTime = arrival,
                Price = random.Next(250, 800),
                SeatCount = random.Next(30, 46)
            });
        }

        await context.BusTrips.AddRangeAsync(trips);
        await context.SaveChangesAsync();
    }
}