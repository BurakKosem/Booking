using System;
using AutoMapper;

namespace Bus.Application.Mappings;

public class BusTripMapping : Profile
{
    public BusTripMapping()
    {
        CreateMap<Domain.Entities.BusTrip, DTOs.BusTripDto>()
            .ForMember(dest => dest.OriginCity, opt => opt.MapFrom(src => src.Route.Origin))
            .ForMember(dest => dest.DestinationCity, opt => opt.MapFrom(src => src.Route.Destination))
            .ForMember(dest => dest.BusCompanyName, opt => opt.MapFrom(src => src.BusCompany.Name))
            .ForMember(dest => dest.BusCompanyPhoneNumber, opt => opt.MapFrom(src => src.BusCompany.PhoneNumber))
            .ForMember(dest => dest.BusCompanyEmail, opt => opt.MapFrom(src => src.BusCompany.Email))
            .ForMember(dest => dest.BusCompanyImage, opt => opt.MapFrom(src => src.BusCompany.Image));
    }
}
