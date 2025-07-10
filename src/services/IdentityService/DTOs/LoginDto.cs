using System;

namespace IdentityService.DTOs;

public record LoginDto
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
