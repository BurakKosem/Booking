using System;

namespace Bus.Domain.ValueObjects;

public class Route
{
    public string Origin { get; set; } = default!;
    public string Destination { get; set; } = default!;
}
