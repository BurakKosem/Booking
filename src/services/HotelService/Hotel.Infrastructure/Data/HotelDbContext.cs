using System;
using HotelService.Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Hotel.Infrastructure.Data;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
    }

    public DbSet<HotelService.Hotel.Domain.Entities.Hotel> Hotels { get; set; } = default!;
    public DbSet<Room> Rooms { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

    }

}
