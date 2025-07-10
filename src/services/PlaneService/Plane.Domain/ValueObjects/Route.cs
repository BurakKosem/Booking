using System;

namespace Plane.Domain.ValueObjects;

public class Route
{
    public Airport Origin { get; set; } = default!;
    public Airport Destination { get; set; } = default!;
}
