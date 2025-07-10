using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Application.DTOs.RoomDTOs;
using HotelService.Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedService.Result;

namespace Hotel.Application.Features.RoomFeatures.Queries.GetAllRoomsByHotelId;

public class GetAllRoomsByHotelIdQueryHandler(HotelDbContext _dbContext, IMapper _mapper) : IRequestHandler<GetAllRoomsByHotelIdQuery, Result<List<RoomDto>>>
{
    public async Task<Result<List<RoomDto>>> Handle(GetAllRoomsByHotelIdQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _dbContext.Rooms
            .Where(r => r.HotelId == request.HotelId)
            .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        if (!rooms.Any())
        {
            return Result<List<RoomDto>>.Failure("Rooms not found for the specified dates.");
        }

        return Result<List<RoomDto>>.Success(rooms);
    }
}
