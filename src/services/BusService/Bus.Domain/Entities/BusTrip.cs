using System;
using Bus.Domain.ValueObjects;

namespace Bus.Domain.Entities;

public class BusTrip : BaseEntity
{
    public Guid BusCompanyId { get; set; }
    public BusCompany BusCompany { get; set; }
    public Route Route { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int Price { get; set; }
    public int SeatCount { get; set; }
}
