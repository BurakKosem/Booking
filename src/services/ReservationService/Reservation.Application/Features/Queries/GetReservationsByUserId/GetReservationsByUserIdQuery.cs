using MediatR;
using Reservation.Application.DTOs;
using SharedService.Result;

namespace Reservation.Application.Features.Queries.GetReservationsByUserId;

public record GetReservationsByUserIdQuery(Guid UserId, string? ReservationType = "", int Page=1, int PageSize=10) : IRequest<Result<PagedResult<ReservationDto>>>;
