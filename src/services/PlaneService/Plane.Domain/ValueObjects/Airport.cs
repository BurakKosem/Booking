using System;

namespace Plane.Domain.ValueObjects;

public record Airport
(
    string Name,
    string City,
    string Country
);
