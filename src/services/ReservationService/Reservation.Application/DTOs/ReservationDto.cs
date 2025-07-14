using Reservation.Domain.Enums;

namespace Reservation.Application.DTOs;

public record ReservationDto
{
    public Guid Id { get; init; }
    public string ReservationNumber { get; init; } = string.Empty;
    public ReservationType ReservationType { get; init; }
    public string Status { get; init; } = string.Empty; 
    public int TotalPrice { get; init; }
    public DateTime ReservationDate { get; init; }
    public DateTime? CancellationDate { get; init; }
    public string? CancellationReason { get; init; }
    public string? SpecialRequests { get; init; }
    public List<ReservationItemDto> ReservationItems { get; init; } = new();
}
