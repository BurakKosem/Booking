using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Plane.Applicaton.DTOs;
using Plane.Infrastructure.Data;
using SharedService.Result;

namespace Plane.Applicaton.Features.SearchFlights;

public class SearchFlightsQueryHandler(PlaneDbContext _dbContext, IMapper _mapper) : IRequestHandler<SearchFlightsQuery, Result<PagedResult<FlightsDto>>>
{
    public async Task<Result<PagedResult<FlightsDto>>> Handle(SearchFlightsQuery request, CancellationToken cancellationToken)
    {
        var dto = request.searchFlightsDto;
        var query = _dbContext.Flights
            .Include(f => f.Airline)
            .Include(f => f.Route.Origin)
            .Include(f => f.Route.Destination)
            .AsQueryable();

        if (!string.IsNullOrEmpty(dto.Origin_AirpotCity))
        {
            query = query.Where(f => f.Route.Origin.City.Trim().ToLower() == dto.Origin_AirpotCity.Trim().ToLower());
        }
        if (!string.IsNullOrEmpty(dto.Destination_AirportCity))
        {
            query = query.Where(f => f.Route.Destination.City.Trim().ToLower() == dto.Destination_AirportCity.Trim().ToLower());
        }
        if (dto.DepartureDate.HasValue)
        {
            query = query.Where(f => f.DepartureTime.Date == dto.DepartureDate.Value.Date);
        }
        if (dto.Price.HasValue && dto.Price > 0)
        {
            query = query.Where(f => f.Price <= dto.Price);
        }
        if (!string.IsNullOrEmpty(dto.AirlineName))
        {
            query = query.Where(f => f.Airline.Name.Trim().ToLower().Contains(dto.AirlineName.Trim().ToLower(), StringComparison.OrdinalIgnoreCase));
        }

        var totalCount = await query.CountAsync();
        var flights = await query
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ProjectTo<FlightsDto>(_mapper.ConfigurationProvider)
            .OrderBy(f => f.DepartureTime)
            .ToListAsync();
        var pagedResult = new PagedResult<FlightsDto>(flights, totalCount, dto.PageNumber, dto.PageSize);

        return Result<PagedResult<FlightsDto>>.Success(pagedResult);
    }
}
