using System;
using AutoMapper;
using Hotel.Application.DTOs.RoomDTOs;
using HotelService.Hotel.Domain.Entities;
using HotelService.Hotel.Domain.ValueObjects;

namespace Hotel.Application.Mappings;

public class RoomMapping : Profile
{
    public RoomMapping()
    {
        CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.HasAirConditioning, opt => opt.MapFrom(src => src.RoomFacilities.HasAirConditioning))
            .ForMember(dest => dest.HasBalcony, opt => opt.MapFrom(src => src.RoomFacilities.HasBalcony))
            .ForMember(dest => dest.HasWifi, opt => opt.MapFrom(src => src.RoomFacilities.HasWifi))
            .ForMember(dest => dest.HasCityView, opt => opt.MapFrom(src => src.RoomFacilities.HasCityView))
            .ForMember(dest => dest.HasMinibar, opt => opt.MapFrom(src => src.RoomFacilities.HasMinibar))
            .ForMember(dest => dest.HasSeaView, opt => opt.MapFrom(src => src.RoomFacilities.HasSeaView))
            .ForMember(dest => dest.NonSmoking, opt => opt.MapFrom(src => src.RoomFacilities.NonSmoking))
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType.ToString()));

        CreateMap<CreateRoomDto, Room>()
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType.ToString()))
            .ForMember(dest => dest.RoomFacilities, opt => opt.MapFrom(src =>
                new RoomFacility
                {
                    HasWifi = src.HasWifi,
                    HasAirConditioning = src.HasAirConditioning,
                    HasMinibar = src.HasMinibar,
                    HasBalcony = src.HasBalcony,
                    HasSeaView = src.HasSeaView,
                    HasCityView = src.HasCityView,
                    NonSmoking = src.NonSmoking
                }))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        CreateMap<UpdateRoomDto, Room>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType.ToString()))
            .ForMember(dest => dest.RoomFacilities, opt => opt.MapFrom(src =>
                new RoomFacility
                {
                    HasWifi = src.HasWifi,
                    HasAirConditioning = src.HasAirConditioning,
                    HasMinibar = src.HasMinibar,
                    HasBalcony = src.HasBalcony,
                    HasSeaView = src.HasSeaView,
                    HasCityView = src.HasCityView,
                    NonSmoking = src.NonSmoking
                }))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
    }
}
