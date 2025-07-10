using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Application.DTOs.RoomDTOs;
using HotelService.Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedService.Result;

namespace Hotel.Application.Features.RoomFeatures.Queries.GetRoomsByRoomType;

public class GetRoomsByRoomTypeQueryHandler(HotelDbContext _dbContext, IMapper _mapper) : IRequestHandler<GetRoomsByRoomTypeQuery, Result<List<RoomDto>>>
{
    public async Task<Result<List<RoomDto>>> Handle(GetRoomsByRoomTypeQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _dbContext.Rooms
            .Where(r => r.HotelId == request.HotelId && r.RoomType.ToString() == request.RoomType)
            .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (!rooms.Any())
        {
            return Result<List<RoomDto>>.Failure("No rooms found for the specified room type.");
        }
        
        return Result<List<RoomDto>>.Success(rooms);
    }
}
