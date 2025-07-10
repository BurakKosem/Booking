namespace Plane.Applicaton.DTOs;

public record CreateFlightDto
(
    string FlightNumber,
    string PlaneName,
    DateTime DepartureTime,
    DateTime ArrivalTime,
    int Price,
    int TotalSeatsCount,

    string AirlineName,
    string AirlineImage,

    string Origin_AirportName,
    string Origin_AirpotCity,
    string Origin_AirportCountry,

    string Destination_AirportName,
    string Destination_AirportCity,
    string Destination_AirportCountry
);
