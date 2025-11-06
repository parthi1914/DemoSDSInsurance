using System.Text.Json.Serialization;

namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad.Auth;

/// <summary>
/// ACE Hub Token Request
/// </summary>
public class AceHubTokenRequest
{
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = string.Empty;

    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; } = string.Empty;
}

/// <summary>
/// ACE Hub Token Response
/// </summary>
public class AceHubTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = "Bearer";

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}
