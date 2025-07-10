using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelService.Hotel.Application.DTOs.HotelDTOs;
using HotelService.Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedService.Result;

namespace HotelService.Hotel.Application.Features.HotelFeatures.Queries.GetHotelById;

public class GetHotelByIdQueryHandler(HotelDbContext _dbContext, IMapper _mapper) : IRequestHandler<GetHotelByIdQuery, Result<HotelDto>>
{
    public async Task<Result<HotelDto>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        var hotel = await _dbContext.Hotels
            .Where(h => h.Id == request.Id)
            .ProjectTo<HotelDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (hotel is null)
        {
            return Result<HotelDto>.Failure("Hotel not found.");
        }

        return Result<HotelDto>.Success(hotel);
    }
}
