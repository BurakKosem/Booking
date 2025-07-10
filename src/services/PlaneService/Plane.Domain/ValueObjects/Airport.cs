using System;

namespace Plane.Domain.ValueObjects;

public class Airport
{
    public string Name { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
}
