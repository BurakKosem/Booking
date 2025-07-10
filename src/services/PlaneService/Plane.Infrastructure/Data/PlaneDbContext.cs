using System;
using Microsoft.EntityFrameworkCore;
using Plane.Domain.Entities;
using Plane.Infrastructure.Configurations;

namespace Plane.Infrastructure.Data;

public class PlaneDbContext : DbContext
{
    public PlaneDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Flight> Flights { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlightEntityConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
