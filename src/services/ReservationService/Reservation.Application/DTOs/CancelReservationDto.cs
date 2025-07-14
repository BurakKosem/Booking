namespace Reservation.Application.DTOs;

public record CancelReservationDto
(
    Guid ReservationId,
    string CancellationReason
);