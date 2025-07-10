using HotelService.Hotel.Application.DTOs.HotelDTOs;
using MediatR;
using SharedService.Result;

namespace HotelService.Hotel.Application.Features.HotelFeatures.Queries.GetAllHotels;

public record GetAllHotelsQuery : IRequest<Result<List<HotelDto>>>
{

}
