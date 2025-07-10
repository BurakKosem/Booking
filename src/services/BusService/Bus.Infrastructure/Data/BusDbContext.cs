using System;
using Bus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bus.Infrastructure.Data;

public class BusDbContext : DbContext
{
    public BusDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BusTrip> BusTrips { get; set; }

    public void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
