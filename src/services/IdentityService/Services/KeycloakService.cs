using System;
using System.Text;
using System.Text.Json;
using IdentityService.DTOs;
using IdentityService.Models;

namespace IdentityService.Services;

public class KeycloakService : IKeycloakService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _realm;
    private readonly string _authServerUrl;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _adminUsername;
    private readonly string _adminPassword;

    public KeycloakService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _realm = _configuration["Keycloak:Realm"];
        _authServerUrl = _configuration["Keycloak:AuthServerUrl"];
        _clientId = _configuration["Keycloak:Resource"];
        _clientSecret = _configuration["Keycloak:Credentials:Secret"];
        _adminUsername = Environment.GetEnvironmentVariable("KEYCLOAK_ADMIN") ?? "admin";
        _adminPassword = Environment.GetEnvironmentVariable("KEYCLOAK_ADMIN_PASSWORD") ?? "admin";
    }

    public async Task<User?> GetUserAsync(string userId)
    {
        var serviceAccountToken = await GetServiceAccountTokenAsync();
        var userEndpoint = $"{_authServerUrl}admin/realms/{_realm}/users/{userId}";

        using var request = new HttpRequestMessage(HttpMethod.Get, userEndpoint);
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", serviceAccountToken);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) return null;

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var userData = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
        return new User
        {
            Id = userData.GetProperty("id").GetString()!,
            Username = userData.GetProperty("username").GetString()!,
            Email = userData.TryGetProperty("email", out var email) ? email.GetString()! : "",
            FirstName = userData.TryGetProperty("firstName", out var firstName) ? firstName.GetString()! : "",
            LastName = userData.TryGetProperty("lastName", out var lastName) ? lastName.GetString()! : "",
        };
    }

    public async Task<TokenResponse> LoginAsync(LoginDto loginDto)
    {
        var tokenEnddpoint = $"{_authServerUrl}realms/{_realm}/protocol/openid-connect/token";
        var parameters = new Dictionary<string, string>
        {
            {"grant_type", "password"},
            {"client_id", _clientId},
            {"client_secret", _clientSecret},
            {"username", loginDto.Username},
            {"password", loginDto.Password}
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = await _httpClient.PostAsync(tokenEnddpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var tokenData = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

        return new TokenResponse
        {
            AccessToken = tokenData.GetProperty("access_token").GetString()!,
            RefreshToken = tokenData.GetProperty("refresh_token").GetString()!,
            ExpiresIn = tokenData.GetProperty("expires_in").GetInt32(),
            TokenType = "Bearer"
        };
    }

    public async Task<bool> LogoutAsync(LogoutDto logoutDto)
    {
        var logoutEndpoint = $"{_authServerUrl}realms/{_realm}/protocol/openid-connect/logout";

        var parameters = new Dictionary<string, string>
        {
            {"client_id", _clientId},
            {"client_secret", _clientSecret},
            {"refresh_token", logoutDto.RefreshToken}
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = await _httpClient.PostAsync(logoutEndpoint, content);

        return response.IsSuccessStatusCode;
    }

    public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var tokenEndpoint = $"{_authServerUrl}realms/{_realm}/protocol/openid-connect/token";
        var parameters = new Dictionary<string, string>
        {
            {"grant_type", "refresh_token"},
            {"client_id", _clientId},
            {"client_secret", _clientSecret},
            {"refresh_token", refreshTokenDto.RefreshToken}
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = await _httpClient.PostAsync(tokenEndpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new UnauthorizedAccessException("Invalid refresh token");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var tokenData = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

        return new TokenResponse
        {
            AccessToken = tokenData.GetProperty("access_token").GetString()!,
            RefreshToken = tokenData.GetProperty("refresh_token").GetString()!,
            ExpiresIn = tokenData.GetProperty("expires_in").GetInt32(),
            TokenType = "Bearer"
        };
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        var serviceAccountToken = await GetServiceAccountTokenAsync();
        var userEndpoint = $"{_authServerUrl}admin/realms/{_realm}/users";

        var user = new
        {
            username = registerDto.Username,
            email = registerDto.Email,
            firstName = registerDto.FirstName,
            lastName = registerDto.LastName,
            enabled = true,
            emailVerified = true,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    value = registerDto.Password,
                    temporary = false
                }
            }
        };

        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var request = new HttpRequestMessage(HttpMethod.Post, userEndpoint);
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", serviceAccountToken);
        request.Content = content;

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Keycloak user creation failed: {response.StatusCode} - {errorContent}");
        }
        return response.IsSuccessStatusCode;
    }

    private async Task<string> GetServiceAccountTokenAsync()
    {
        var tokenEndpoint = $"{_authServerUrl}realms/{_realm}/protocol/openid-connect/token";

        var parameters = new Dictionary<string, string>
    {
        {"grant_type", "client_credentials"},
        {"client_id", _clientId},
        {"client_secret", _clientSecret}
    };

        var content = new FormUrlEncodedContent(parameters);
        var response = await _httpClient.PostAsync(tokenEndpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new UnauthorizedAccessException($"Failed to get service account token: {errorContent}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var tokenData = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

        return tokenData.GetProperty("access_token").GetString()!;
    }
}
