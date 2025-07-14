using System;

namespace Reservation.Domain.Services;

public interface IReservationDomainService
{
    Task<bool> CanCreateReservationAsync(Guid userId, Guid serviceId, DateTime serviceDate);
    Task<int> CalculatePricingAsync(int baseAmount, int adultCount, int childCount);
    Task<bool> IsServiceAvailableAsync(Guid serviceId, DateTime serviceDate, int quantity);

}
