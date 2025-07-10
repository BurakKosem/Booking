using AutoMapper;
using HotelService.Hotel.Application.DTOs.HotelDTOs;
using HotelService.Hotel.Domain.Enums;
using HotelService.Hotel.Domain.ValueObjects;

namespace HotelService.Hotel.Application.Mappings;

public class HotelMapping : Profile
{
    public HotelMapping()
    {
        CreateMap<HotelService.Hotel.Domain.Entities.Hotel, HotelDto>()
            .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.Address.District))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.HasPool, opt => opt.MapFrom(src => src.Facilities.HasPool))
            .ForMember(dest => dest.HasGym, opt => opt.MapFrom(src => src.Facilities.HasGym))
            .ForMember(dest => dest.HasSpa, opt => opt.MapFrom(src => src.Facilities.HasSpa))
            .ForMember(dest => dest.HasRestaurant, opt => opt.MapFrom(src => src.Facilities.HasRestaurant))
            .ForMember(dest => dest.HasBar, opt => opt.MapFrom(src => src.Facilities.HasBar))
            .ForMember(dest => dest.HasConferenceRoom, opt => opt.MapFrom(src => src.Facilities.HasConferenceRoom))
            .ForMember(dest => dest.PetFriendly, opt => opt.MapFrom(src => src.Facilities.PetFriendly))
            .ForMember(dest => dest.HotelType, opt => opt.MapFrom(src => src.HotelType.ToString()))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0))
            .ForMember(dest => dest.TotalReviews, opt => opt.MapFrom(src => src.Reviews.Count))
            .ForMember(dest => dest.TotalRooms, opt => opt.MapFrom(src => src.Rooms.Count))
            .ForMember(dest => dest.MinRoomPrice, opt => opt.MapFrom(src =>
                src.Rooms.Any() ? src.Rooms.Min(r => r.BasePrice) : (decimal?)null));

        CreateMap<CreateHotelDto, HotelService.Hotel.Domain.Entities.Hotel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? string.Empty))
            .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber ?? string.Empty))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(src.Street, src.District, src.City)))
            .ForMember(dest => dest.Facilities, opt => opt.MapFrom(src => new HotelFacility
            {
                HasPool = src.HasPool,
                HasGym = src.HasGym,
                HasSpa = src.HasSpa,
                HasRestaurant = src.HasRestaurant,
                HasBar = src.HasBar,
                HasConferenceRoom = src.HasConferenceRoom,
                PetFriendly = src.PetFriendly
            }))
            .ForMember(dest => dest.HotelType, opt => opt.MapFrom(src => Enum.Parse<HotelType>(src.HotelType)))
            .ForMember(dest => dest.CheckInTime, opt => opt.MapFrom(src => src.CheckInTime ?? new TimeOnly(14, 0)))
            .ForMember(dest => dest.CheckOutTime, opt => opt.MapFrom(src => src.CheckOutTime ?? new TimeOnly(12, 0)))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Rooms, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore());

        CreateMap<UpdateHotelDto, HotelService.Hotel.Domain.Entities.Hotel>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) 
            .ForMember(dest => dest.Rooms, opt => opt.Ignore()) 
            .ForMember(dest => dest.Reviews, opt => opt.Ignore()) 
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(src.Street, src.District, src.City)))
            .ForMember(dest => dest.Facilities, opt => opt.MapFrom(src => new HotelFacility
            {
                HasPool = src.HasPool,
                HasGym = src.HasGym,
                HasSpa = src.HasSpa,
                HasRestaurant = src.HasRestaurant,
                HasBar = src.HasBar,
                HasConferenceRoom = src.HasConferenceRoom,
                PetFriendly = src.PetFriendly
            }))
            .ForMember(dest => dest.HotelType, opt => opt.MapFrom(src => Enum.Parse<HotelType>(src.HotelType)))
            .ForMember(dest => dest.CheckInTime, opt => opt.MapFrom(src => src.CheckInTime ?? new TimeOnly(14, 0)))
            .ForMember(dest => dest.CheckOutTime, opt => opt.MapFrom(src => src.CheckOutTime ?? new TimeOnly(12, 0)));

    }
}
