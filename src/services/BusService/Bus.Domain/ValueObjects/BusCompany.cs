using System;

namespace Bus.Domain.ValueObjects;

public class BusCompany
{
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Image { get; set; } = default!;
}
