using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelService.Hotel.Application.DTOs.HotelDTOs;
using HotelService.Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedService.Result;

namespace HotelService.Hotel.Application.Features.HotelFeatures.Queries.GetAllHotels;

public class GetAllHotelsQueryHandler(HotelDbContext _dbContext, IMapper _mapper) : IRequestHandler<GetAllHotelsQuery, Result<List<HotelDto>>>
{
    public async Task<Result<List<HotelDto>>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        var hotels = await _dbContext.Hotels
            .ProjectTo<HotelDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        if (hotels is null || hotels.Count == 0)
        {
            return Result<List<HotelDto>>.Failure("Hotels not found");
        }
        return Result<List<HotelDto>>.Success(hotels);
    }
}
