using System;
using AutoMapper;
using Reservation.Application.DTOs;
using Reservation.Domain.Entities;

namespace Reservation.Application.Mappings;

public class ReservationMapping : Profile
{
    public ReservationMapping()
    {
        CreateMap<ReservationItem, ReservationItemDto>()
            .ForMember(dest => dest.ItemDetails, opt => opt.MapFrom(src => src.ItemDetails.Properties));

        CreateMap<Domain.Entities.Reservation, ReservationDto>()
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
        .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Price));
    
    }
}
