using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Application.DTOs.RoomDTOs;
using HotelService.Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedService.Result;

namespace Hotel.Application.Features.RoomFeatures.Queries.GetRoomById;

public class GetRoomByIdQueryHandler(HotelDbContext _dbContext, IMapper _mapper) : IRequestHandler<GetRoomByIdQuery, Result<RoomDto>>
{
    public async Task<Result<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _dbContext.Rooms
            .Where(r => r.Id == request.Id)
            .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (room is null)
        {
            return Result<RoomDto>.Failure("Room not found.");
        }
        
        return Result<RoomDto>.Success(room);
    }
}
