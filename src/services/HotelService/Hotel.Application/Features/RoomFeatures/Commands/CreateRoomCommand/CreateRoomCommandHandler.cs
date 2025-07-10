using System;
using AutoMapper;
using HotelService.Hotel.Domain.Entities;
using HotelService.Hotel.Infrastructure.Data;
using MediatR;
using SharedService.Result;

namespace Hotel.Application.Features.RoomFeatures.Commands.CreateRoomCommand;

public class CreateRoomCommandHandler(HotelDbContext _dbContext, IMapper _mapper) : IRequestHandler<CreateRoomCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var mappedRoom = _mapper.Map<Room>(request.CreateRoomDto);
        
        await _dbContext.Rooms.AddAsync(mappedRoom);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        if (result <= 0)
        {
            return Result<bool>.Failure("Failed to create room.");
        }

        return Result<bool>.Success(true);
    }
}
