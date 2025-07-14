using MediatR;
using Reservation.Application.DTOs;
using SharedService.Result;

namespace Reservation.Application.Features.Commands.CreateReservation;

public record CreateReservationCommand(CreateReservationDto createReservationDto) : IRequest<Result<Guid>>;