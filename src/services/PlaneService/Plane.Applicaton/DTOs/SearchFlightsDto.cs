namespace Plane.Applicaton.DTOs;

public record SearchFlightsDto
(
    DateTime? DepartureDate = null,
    int? Price = 0,
    string? AirlineName = "",
    string? Origin_AirpotCity = "",
    string? Destination_AirportCity = "",

    int PageNumber = 1,
    int PageSize = 10
);