using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.DTOs;
using Reservation.Infrastructure.Data;
using SharedService.Result;

namespace Reservation.Application.Features.Queries.GetReservationsByUserId;

public class GetReservationsByUserIdQueryHandler(ReservationDbContext _dbContext, IMapper _mapper) : IRequestHandler<GetReservationsByUserIdQuery, Result<PagedResult<ReservationDto>>>
{
    public async Task<Result<PagedResult<ReservationDto>>> Handle(GetReservationsByUserIdQuery request, CancellationToken cancellationToken)
    {

        var query = _dbContext.Reservations
            .Where(r => r.UserId == request.UserId)
            .Include(r => r.ReservationItems)
            .AsNoTracking();
            

        if (!string.IsNullOrEmpty(request.ReservationType))
        {
            query = query.Where(r => r.ReservationType.ToString() == request.ReservationType);
        }

        if (query == null || !query.Any())
        {
            return Result<PagedResult<ReservationDto>>.Failure("No reservations found for the specified user.");
        }
        
        var totalCount = await query.CountAsync(cancellationToken);

        var reservations = await query
            .OrderByDescending(r => r.ReservationDate) 
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<ReservationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);


        var pagedResult = new PagedResult<ReservationDto>(reservations, totalCount, request.Page, request.PageSize);

        return Result<PagedResult<ReservationDto>>.Success(pagedResult);
    }
}
