using Reservation.Domain.Enums;

namespace Reservation.Application.DTOs;

public record CreateReservationDto
(
    Guid UserId,
    string ReservationType,
    string CustomerFirstName,
    string CustomerLastName,
    string CustomerEmail,
    List<CreateReservationItemDto> Items,
    string? SpecialRequests
);
