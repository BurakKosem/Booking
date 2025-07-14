namespace Reservation.Domain.Events;

public record ReservationConfirmedEvent(Guid ReservationId, string ReservationNumber) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}