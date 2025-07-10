using Hotel.Application.DTOs.RoomDTOs;
using MediatR;
using SharedService.Result;

namespace Hotel.Application.Features.RoomFeatures.Queries.GetRoomsByRoomType;

public record GetRoomsByRoomTypeQuery(Guid HotelId, string RoomType) : IRequest<Result<List<RoomDto>>>;