using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservation.Domain.Entities;

namespace Reservation.Infrastructure.Configurations;

public class ReservationItemEntityConfiguration : IEntityTypeConfiguration<ReservationItem>
{
    public void Configure(EntityTypeBuilder<ReservationItem> builder)
    {
        builder.ToTable("ReservationItems");

        builder.HasKey(ri => ri.Id);

        builder.Property(ri => ri.ItemType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.OwnsOne(ri => ri.ItemDetails, details =>
        {
            details.Property(d => d.ServiceName).HasColumnName("ServiceName").HasMaxLength(100);
            details.Property(d => d.Properties).HasColumnType("jsonb"); 
        });
    }
}
