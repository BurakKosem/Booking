using Hotel.Application.DTOs.RoomDTOs;
using MediatR;
using SharedService.Result;

namespace Hotel.Application.Features.RoomFeatures.Queries.GetAllRoomsByHotelId;

public record GetAllRoomsByHotelIdQuery(Guid HotelId) : IRequest<Result<List<RoomDto>>>;
