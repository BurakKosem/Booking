using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bus.Application.DTOs;
using Bus.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedService.Result;

namespace Bus.Application.Features.SearchBusTrips;

public class SearchBusTripsQueryHandler(BusDbContext _dbContext, IMapper _mapper) : IRequestHandler<SearchBusTripsQuery, Result<PagedResult<BusTripDto>>>
{
    public async Task<Result<PagedResult<BusTripDto>>> Handle(SearchBusTripsQuery request, CancellationToken cancellationToken)
    {
        var dto = request.searchBusTripsDto;
        var busQuery = _dbContext.BusTrips
            .Include(b => b.BusCompany)
            .Include(b => b.Route)
            .AsQueryable();

        if (!string.IsNullOrEmpty(dto.OriginCity))
        {
            busQuery = busQuery.Where(b => b.Route.Origin.Trim().ToLower().Contains(dto.OriginCity.Trim().ToLower(), StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(dto.DestinationCity))
        {
            busQuery = busQuery.Where(b => b.Route.Destination.Trim().ToLower().Contains(dto.DestinationCity.Trim().ToLower(), StringComparison.OrdinalIgnoreCase));
        }
        if (dto.Price > 0)
        {
            busQuery = busQuery.Where(b => b.Price <= dto.Price);
        }
        if (dto.DepartureDate.HasValue)
        {
            busQuery = busQuery.Where(b => b.DepartureTime.Date == dto.DepartureDate.Value.Date);
        }

        var totalCount = await busQuery.CountAsync();
        
         var busTrips = await busQuery
            .OrderBy(h => h.Price)
            .Skip((dto.Page - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ProjectTo<BusTripDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var pagedResult = new PagedResult<BusTripDto>(busTrips, totalCount, dto.Page, dto.PageSize);

        return Result<PagedResult<BusTripDto>>.Success(pagedResult);
    }
}
