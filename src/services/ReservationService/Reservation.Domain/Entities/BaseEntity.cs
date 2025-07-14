using System;

namespace Reservation.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
