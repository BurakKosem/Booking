using System;
using Reservation.Domain.Enums;
using Reservation.Domain.ValueObjects;

namespace Reservation.Domain.Entities;

public class ReservationItem : BaseEntity
{
    public Guid ReservationId { get; private set; }
    public Reservation Reservation { get; private set; }

    public Guid ServiceId { get; private set; }
    public Guid? SubServiceId { get; private set; }

    public ItemDetails ItemDetails { get; private set; }
    public int UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public int TotalPrice { get; private set; }

    public DateTime? ServiceDate { get; private set; }
    public DateTime? EndDate { get; private set; }


    private ReservationItem() { }
    
    public static ReservationItem Create(
        Guid reservationId,
        Guid serviceId,
        Guid? subServiceId,
        ItemDetails itemDetails,
        int unitPrice,
        int quantity,
        DateTime? serviceDate = null,
        DateTime? endDate = null)
    {
        var reservationItem = new ReservationItem
        {
            ReservationId = reservationId,
            ServiceId = serviceId,
            SubServiceId = subServiceId,
            ItemDetails = itemDetails,
            UnitPrice = unitPrice,
            Quantity = quantity,
            TotalPrice = unitPrice * quantity,
            ServiceDate = serviceDate,
            EndDate = endDate
        };

        return reservationItem;
    }
}
