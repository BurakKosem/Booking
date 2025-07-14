using System;

namespace Reservation.Domain.Events;

public interface IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }
}
