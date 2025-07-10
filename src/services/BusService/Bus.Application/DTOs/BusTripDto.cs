namespace Bus.Application.DTOs;

public record BusTripDto
(
    Guid Id,
    DateTime DepartureTime,
    DateTime ArrivalTime,
    int Price,

    string BusCompanyName,
    string BusCompanyPhoneNumber,
    string BusCompanyEmail,
    string BusCompanyImage,

    string OriginCity,
    string DestinationCity,

    int TotalSeatsCount
);