using System;
using Bus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bus.Infrastructure.Configurations;

public class BusTripEntityConfiguration : IEntityTypeConfiguration<BusTrip>
{
    public void Configure(EntityTypeBuilder<BusTrip> builder)
    {
        builder.ToTable("BusTrips");

        builder.HasKey(bt => bt.Id);

        builder.OwnsOne(bt => bt.BusCompany, bc =>
        {
            bc.Property(bc => bc.Name).IsRequired().HasMaxLength(100);
            bc.Property(bc => bc.PhoneNumber).IsRequired().HasMaxLength(15);
            bc.Property(bc => bc.Email).IsRequired().HasMaxLength(100);
            bc.Property(bc => bc.Image).HasMaxLength(200);
        });
    }
}
