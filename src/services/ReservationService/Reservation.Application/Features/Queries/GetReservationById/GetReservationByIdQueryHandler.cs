using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.DTOs;
using Reservation.Infrastructure.Data;
using SharedService.Result;

namespace Reservation.Application.Features.Queries.GetReservationById;

public class GetReservationByIdQueryHandler(ReservationDbContext _dbContext, IMapper _mapper) : IRequestHandler<GetReservationByIdQuery, Result<ReservationDto>>
{
    public async Task<Result<ReservationDto>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {

        var reservation = await _dbContext.Reservations
            .Where(r => r.Id == request.Id)
            .Include(r => r.ReservationItems)
            .ProjectTo<ReservationDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (reservation == null)
        {
            return Result<ReservationDto>.Failure("Reservation not found.");
        }
        
        return Result<ReservationDto>.Success(reservation);
    }
}
