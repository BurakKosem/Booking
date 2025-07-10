using System;

namespace IdentityService.Models;

public class User
{
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime CreatedTimestamp { get; set; }
    public List<string> Roles { get; set; } = new();
}
