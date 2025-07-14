using Reservation.Domain.Enums;

namespace Reservation.Application.DTOs;

public record ReservationItemDto
{
    public Guid Id { get; init; }
    public Guid ServiceId { get; init; }
    public Guid? SubServiceId { get; init; }
    public int Quantity { get; init; }
    public int TotalPrice { get; init; }
    public DateTime ServiceDate { get; init; }
    public DateTime? EndDate { get; init; }
    public Dictionary<string, string> ItemDetails { get; init; } = new();
}