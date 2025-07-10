namespace IdentityService.DTOs;

public record RefreshTokenDto
{
    public string RefreshToken { get; set; } = default!;
}
