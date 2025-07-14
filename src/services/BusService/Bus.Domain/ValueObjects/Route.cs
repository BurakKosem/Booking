using System;

namespace Bus.Domain.ValueObjects;

public record Route
(
    string Origin,
    string Destination
);
