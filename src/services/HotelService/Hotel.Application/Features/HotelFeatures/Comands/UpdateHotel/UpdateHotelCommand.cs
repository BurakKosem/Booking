using HotelService.Hotel.Application.DTOs.HotelDTOs;
using MediatR;
using SharedService.Result;

namespace HotelService.Hotel.Application.Features.HotelFeatures.Comands.UpdateHotel;

public record UpdateHotelCommand(UpdateHotelDto UpdateHotelDto) : IRequest<Result<bool>>;
