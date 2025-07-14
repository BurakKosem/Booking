using System;
using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Entities;

namespace Reservation.Infrastructure.Data;

public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Reservation> Reservations { get; set; }
    public DbSet<ReservationItem> ReservationItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
