using Hotel.Application.DTOs.RoomDTOs;
using MediatR;
using SharedService.Result;

namespace Hotel.Application.Features.RoomFeatures.Queries.GetRoomById;

public record GetRoomByIdQuery(Guid Id) : IRequest<Result<RoomDto>>;
