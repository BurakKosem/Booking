using IdentityService.DTOs;
using IdentityService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IKeycloakService _keycloakService;
        public AuthController(IKeycloakService keycloakService)
        {
            _keycloakService = keycloakService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var tokenResponse = await _keycloakService.LoginAsync(loginDto);
                if (tokenResponse == null)
                {
                    return Unauthorized("Invalid credentials");
                }
                return Ok(tokenResponse);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerUserDto)
        {
            try
            {
                var result = await _keycloakService.RegisterAsync(registerUserDto);
                if (!result)
                {
                    return BadRequest("Registration failed");
                }
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            try
            {
                var tokenResponse = await _keycloakService.RefreshTokenAsync(refreshTokenDto);
                if (tokenResponse == null)
                {
                    return Unauthorized("Invalid refresh token");
                }
                return Ok(tokenResponse);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid refresh token");
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody] LogoutDto logoutDto)
        {
            try
            {
                var result = await _keycloakService.LogoutAsync(logoutDto);
                if (!result)
                {
                    return BadRequest("Logout failed");
                }
                return Ok("User logged out successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
