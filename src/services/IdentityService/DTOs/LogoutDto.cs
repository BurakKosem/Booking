namespace IdentityService.DTOs;

public record LogoutDto
{
    public string RefreshToken { get; set; } = default!;
}
