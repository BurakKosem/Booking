using System;
using Reservation.Domain.Enums;
using Reservation.Domain.Events;
using Reservation.Domain.ValueObjects;

namespace Reservation.Domain.Entities;

public class Reservation : AggregateRoot
{
    public string ReservationNumber { get; private set; }
    public Guid UserId { get; private set; }
    public ReservationType ReservationType { get; private set; }
    public ReservationStatus Status { get; private set; }

    public CustomerInfo Customer { get; private set; }
    public int Price { get; private set; }

    public DateTime ReservationDate { get; private set; }
    public DateTime? CheckInDate { get; private set; }
    public DateTime? CheckOutDate { get; private set; }
    public DateTime? CancellationDate { get; private set; }

    public string? SpecialRequests { get; private set; }
    public string? CancellationReason { get; private set; }

    public ICollection<ReservationItem> ReservationItems { get; private set; } = new HashSet<ReservationItem>();

    private Reservation() { }

    public static Reservation Create(
        Guid userId,
        ReservationType reservationType,
        CustomerInfo customer,
        int price,
        DateTime? checkInDate = null,
        DateTime? checkOutDate = null,
        string? specialRequests = null)
    {
        var reservation = new Reservation
        {
            ReservationNumber = GenerateReservationNumber(reservationType),
            UserId = userId,
            ReservationType = reservationType,
            Customer = customer,
            Price = price,
            ReservationDate = DateTime.UtcNow,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
            SpecialRequests = specialRequests,
            Status = ReservationStatus.Pending
        };

        reservation.AddDomainEvent(new ReservationCreatedEvent(reservation.Id, reservation.ReservationNumber));
        return reservation;
    }

    public void Confirm()
    {
        if (Status != ReservationStatus.Pending)
            throw new InvalidOperationException("Only pending reservations can be confirmed.");

        Status = ReservationStatus.Confirmed;
        AddDomainEvent(new ReservationConfirmedEvent(Id, ReservationNumber));
    }

    public void Cancel(string reason)
    {
        if (Status == ReservationStatus.Cancelled)
            throw new InvalidOperationException("Reservation is already cancelled.");

        Status = ReservationStatus.Cancelled;
        CancellationDate = DateTime.UtcNow;
        CancellationReason = reason;
        AddDomainEvent(new ReservationCancelledEvent(Id, ReservationNumber, reason));
    }

    public void AddReservationItem(ReservationItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        ReservationItems.Add(item);
    }
    private static string GenerateReservationNumber(ReservationType type)
    {
        var prefix = type switch
        {
            ReservationType.Hotel => "HTL",
            ReservationType.Bus => "BUS",
            ReservationType.Plane => "PLN",
            _ => "RES"
        };

        return $"{prefix}{DateTime.UtcNow:yyyyMMdd}{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
    }
}



