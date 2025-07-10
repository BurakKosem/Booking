using System;
using HotelService.Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelService.Hotel.Infrastructure.Configurations;

public class RoomEntityConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> entity)
    {
        entity.HasKey(r => r.Id);
        entity.Property(r => r.Id).ValueGeneratedOnAdd();

        entity.OwnsOne(r => r.RoomFacilities, facilities =>
        {
            facilities.Property(f => f.HasWifi).HasColumnName("HasWifi");
            facilities.Property(f => f.HasAirConditioning).HasColumnName("HasAirConditioning");
            facilities.Property(f => f.HasMinibar).HasColumnName("HasMinibar");
            facilities.Property(f => f.HasBalcony).HasColumnName("HasBalcony");
            facilities.Property(f => f.HasSeaView).HasColumnName("HasSeaView");
            facilities.Property(f => f.HasCityView).HasColumnName("HasCityView");
            facilities.Property(f => f.NonSmoking).HasColumnName("NonSmoking");
        });
        
        entity.HasIndex(r => new { r.HotelId }).IsUnique();

    }
}
