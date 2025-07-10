using HotelService.Hotel.Application.DTOs.HotelDTOs;
using MediatR;
using SharedService.Result;

namespace HotelService.Hotel.Application.Features.HotelFeatures.Queries.GetHotelById;

public record GetHotelByIdQuery(Guid Id) : IRequest<Result<HotelDto>>;
