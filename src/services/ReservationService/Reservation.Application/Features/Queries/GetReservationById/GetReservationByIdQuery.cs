using MediatR;
using Reservation.Application.DTOs;
using SharedService.Result;

namespace Reservation.Application.Features.Queries.GetReservationById;

public record GetReservationByIdQuery(Guid Id) : IRequest<Result<ReservationDto>>;