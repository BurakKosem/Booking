using MediatR;
using Reservation.Application.DTOs;
using SharedService.Result;

namespace Reservation.Application.Features.Commands.CancelReservation;

public record CancelReservationCommand(CancelReservationDto cancelReservationDto) : IRequest<Result<bool>>;