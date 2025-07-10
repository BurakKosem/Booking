using Hotel.Application.DTOs.RoomDTOs;
using MediatR;
using SharedService.Result;

namespace Hotel.Application.Features.RoomFeatures.Commands.CreateRoomCommand;

public record CreateRoomCommand(CreateRoomDto CreateRoomDto) : IRequest<Result<bool>>;
