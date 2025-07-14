using System;
using MediatR;
using Reservation.Domain.Enums;
using Reservation.Infrastructure.Data;
using SharedService.Result;

namespace Reservation.Application.Features.Commands.CancelReservation;

public class CancelReservationCommandHandler(ReservationDbContext _dbContext) : IRequestHandler<CancelReservationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _dbContext.Reservations.FindAsync(request.cancelReservationDto.ReservationId);

        if (reservation == null)
        {
            return Result<bool>.Failure("Reservation not found.");
        }

        try
        {
            reservation.Cancel(request.cancelReservationDto.CancellationReason);
        }
        catch(InvalidOperationException ex)
        {
            return Result<bool>.Failure(ex.Message);
        }

        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        if (result <= 0)
        {
            return Result<bool>.Failure("Failed to cancel reservation.");
        }

        return Result<bool>.Success(true);
    }
}
