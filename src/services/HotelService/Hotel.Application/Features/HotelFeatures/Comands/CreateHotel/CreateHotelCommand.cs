using HotelService.Hotel.Application.DTOs.HotelDTOs;
using MediatR;
using SharedService.Result;

namespace HotelService.Hotel.Application.Features.HotelFeatures.Comands.CreateHotel;

public record CreateHotelCommand(CreateHotelDto createHotelDto) : IRequest<Result<Guid>>;
