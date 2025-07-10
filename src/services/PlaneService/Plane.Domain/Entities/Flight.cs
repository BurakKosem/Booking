using System;
using Plane.Domain.ValueObjects;

namespace Plane.Domain.Entities;

public class Flight : BaseEntity
{
    public string FlightNumber { get; set; } = default!;
    public string PlaneName { get; set; } = default!;
    public Airline Airline { get; set; } = default!;
    public Route Route { get; set; } = default!;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int Price { get; set; }
    public int TotalSeatsCount { get; set; }
}
