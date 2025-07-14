using System;

namespace Bus.Domain.ValueObjects;

public record BusCompany
(
    string Name,
    string PhoneNumber,
    string Email,
    string Image
);