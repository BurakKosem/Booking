using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelService.Hotel.Infrastructure.Configurations;

public class HotelEntityConfiguration : IEntityTypeConfiguration<HotelService.Hotel.Domain.Entities.Hotel>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Hotel> entity)
    {
        entity.HasKey(h => h.Id);
        entity.Property(h => h.Id).ValueGeneratedOnAdd();

        entity.Property(h => h.Name).IsRequired().HasMaxLength(200);
        entity.Property(h => h.Description).HasMaxLength(2000);
        entity.Property(h => h.PhoneNumber).HasMaxLength(20);
        entity.Property(h => h.Stars).IsRequired();

        entity.OwnsOne(h => h.Address, address =>
        {
            address.Property(a => a.Street).HasColumnName("Address_Street").HasMaxLength(200);
            address.Property(a => a.City).HasColumnName("Address_City").HasMaxLength(100);
            address.Property(a => a.District).HasColumnName("Address_District").HasMaxLength(100);
        });

        entity.OwnsOne(h => h.Facilities, facilities =>
        {
            facilities.Property(f => f.HasPool).HasColumnName("HasPool");
            facilities.Property(f => f.HasGym).HasColumnName("HasGym");
            facilities.Property(f => f.HasSpa).HasColumnName("HasSpa");
            facilities.Property(f => f.HasRestaurant).HasColumnName("HasRestaurant");
            facilities.Property(f => f.HasBar).HasColumnName("HasBar");
            facilities.Property(f => f.HasConferenceRoom).HasColumnName("HasConferenceRoom");
            facilities.Property(f => f.PetFriendly).HasColumnName("PetFriendly");
        });

        entity.HasMany(h => h.Rooms).WithOne(r => r.Hotel).HasForeignKey(r => r.HotelId);
        entity.HasMany(h => h.Reviews).WithOne(r => r.Hotel).HasForeignKey(r => r.HotelId);
    }
}
