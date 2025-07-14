namespace Reservation.Domain.Events;

public record ReservationCancelledEvent(Guid ReservationId, string ReservationNumber, string Reason) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
