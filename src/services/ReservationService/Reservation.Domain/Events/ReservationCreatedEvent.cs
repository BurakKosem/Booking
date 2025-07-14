namespace Reservation.Domain.Events;

public record ReservationCreatedEvent(Guid ReservationId, string ReservationNumber) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccuredOn { get; set; } = DateTime.Now;
}
