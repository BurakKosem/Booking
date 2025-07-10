using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelService.Hotel.Application.DTOs.HotelDTOs;
using HotelService.Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedService.Result;

namespace Hotel.Application.Features.HotelFeatures.Queries.SearchHotel;

public class SearchHotelQueryHandler(HotelDbContext _dbContext, IMapper _mapper) : IRequestHandler<SearchHotelQuery, Result<PagedResult<HotelDto>>>
{
    public async Task<Result<PagedResult<HotelDto>>> Handle(SearchHotelQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Hotels.AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.SearchHotelDto.Keyword))
        {
            var keyword = request.SearchHotelDto.Keyword.Trim().ToLower();
            query = query.Where(h =>
                h.Name.ToLower().Contains(keyword) ||
                h.Address.City.ToLower().Contains(keyword) ||
                h.Address.District.ToLower().Contains(keyword) ||
                h.HotelType.ToString().ToLower().Contains(keyword)
            );
        }

        if (!string.IsNullOrEmpty(request.SearchHotelDto.Name))
        {
            query = query.Where(h => h.Name.Trim().ToLower().Contains(request.SearchHotelDto.Name.Trim().ToLower(), StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(request.SearchHotelDto.City))
        {
            query = query.Where(h => h.Address.City.Trim().ToLower().Contains(request.SearchHotelDto.City.Trim().ToLower(), StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(request.SearchHotelDto.District))
        {
            query = query.Where(h => h.Address.District.Trim().ToLower().Contains(request.SearchHotelDto.District.Trim().ToLower(), StringComparison.OrdinalIgnoreCase));
        }
        if (request.SearchHotelDto.Stars != null)
        {
            query = query.Where(h => h.Stars == request.SearchHotelDto.Stars);
        }
        if (request.SearchHotelDto.MinRoomPrice.HasValue)
            query = query.Where(h => h.Rooms.Any(r => r.BasePrice >= request.SearchHotelDto.MinRoomPrice.Value));

        var totalCount = await query.CountAsync(cancellationToken);

        var hotels = await query
            .OrderBy(h => h.Name)
            .Skip((request.SearchHotelDto.Page - 1) * request.SearchHotelDto.PageSize)
            .Take(request.SearchHotelDto.PageSize)
            .ProjectTo<HotelDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var pagedResult = new PagedResult<HotelDto>(hotels, totalCount, request.SearchHotelDto.Page, request.SearchHotelDto.PageSize);

        return Result<PagedResult<HotelDto>>.Success(pagedResult);

    }
}
