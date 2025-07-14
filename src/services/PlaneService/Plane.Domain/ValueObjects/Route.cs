using System;

namespace Plane.Domain.ValueObjects;

public record Route
(
    Airport Origin,
    Airport Destination
);