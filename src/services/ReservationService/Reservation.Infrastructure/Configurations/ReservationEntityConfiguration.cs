using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reservation.Infrastructure.Configurations;

public class ReservationEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.Reservation>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReservationNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(r => r.ReservationNumber).IsUnique();

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>() 
            .HasMaxLength(50);

        builder.Property(r => r.ReservationType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.OwnsOne(r => r.Customer, customer =>
        {
            customer.Property(c => c.FirstName).HasColumnName("CustomerFirstName").HasMaxLength(100).IsRequired();
            customer.Property(c => c.LastName).HasColumnName("CustomerLastName").HasMaxLength(100).IsRequired();
            customer.Property(c => c.Email).HasColumnName("CustomerEmail").HasMaxLength(150).IsRequired();
        });

        builder.HasMany(r => r.ReservationItems)
            .WithOne(ri => ri.Reservation)
            .HasForeignKey(ri => ri.ReservationId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
