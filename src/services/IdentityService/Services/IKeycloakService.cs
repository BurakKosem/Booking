using System;
using IdentityService.DTOs;
using IdentityService.Models;

namespace IdentityService.Services;

public interface IKeycloakService
{
    Task<TokenResponse> LoginAsync(LoginDto loginDto);
    Task<bool> RegisterAsync(RegisterDto registerUserDto);
    Task<User?> GetUserAsync(string userId);
    Task<TokenResponse> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    Task<bool> LogoutAsync(LogoutDto logoutDto);
}
