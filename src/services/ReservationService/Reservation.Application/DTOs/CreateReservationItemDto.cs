using Reservation.Domain.Enums;

namespace Reservation.Application.DTOs;

public record CreateReservationItemDto
(
    Guid ServiceId,         
    Guid? SubServiceId,      
    int Quantity,
    int UnitPrice,
    DateTime ServiceDate,  
    DateTime? EndDate,      
    Dictionary<string, string>? AdditionalInfo
);