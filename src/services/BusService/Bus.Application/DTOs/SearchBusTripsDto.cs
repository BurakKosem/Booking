namespace Bus.Application.DTOs;

public record SearchBusTripsDto
(
    string OriginCity = "",
    string DestinationCity = "",
    int Price = 0,
    DateTime? DepartureDate = null,
    int Page = 1,
    int PageSize = 10
);
