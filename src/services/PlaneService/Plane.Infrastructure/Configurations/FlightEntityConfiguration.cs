using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plane.Domain.Entities;

namespace Plane.Infrastructure.Configurations;

public class FlightEntityConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.OwnsOne(f => f.Airline, airline =>
        {
            airline.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsOne(f => f.Route, route =>
        {
            route.OwnsOne(r => r.Origin, origin =>
            {
                origin.Property(o => o.Name).IsRequired().HasMaxLength(100);
                origin.Property(o => o.City).IsRequired().HasMaxLength(100);
                origin.Property(o => o.Country).IsRequired().HasMaxLength(100);
            });
            route.OwnsOne(r => r.Destination, dest =>
            {
                dest.Property(d => d.Name).IsRequired().HasMaxLength(100);
                dest.Property(d => d.City).IsRequired().HasMaxLength(100);
                dest.Property(d => d.Country).IsRequired().HasMaxLength(100);
            });
        });

    }
}
