using Refit;
using InsuranceWrapperApi.Application.DTOs.Providers.Dyad;
using InsuranceWrapperApi.Application.DTOs.Providers.Dyad.Auth;

namespace InsuranceWrapperApi.Infrastructure.ApiClients;

/// <summary>
/// ACE Hub (Dyad) API Client using Refit
/// </summary>
public interface IAceHubApiClient
{
    /// <summary>
    /// Get OAuth token for authentication
    /// </summary>
    [Post("/acehub/GetToken")]
    Task<AceHubTokenResponse> GetTokenAsync(
        [Body] AceHubTokenRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get insurance rate/quote from multiple carriers
    /// </summary>
    [Post("/acehub/GetRate")]
    [Headers("Authorization: Bearer")]
    Task<AceHubRateResponse> GetRateAsync(
        [Body] AceHubRateRequest request,
        [Header("Authorization")] string authorization,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get policy documents (quote, policy, binder, etc.)
    /// </summary>
    [Post("/acehub/GetDocument")]
    [Headers("Authorization: Bearer")]
    Task<AceHubDocumentResponse> GetDocumentAsync(
        [Body] AceHubDocumentRequest request,
        [Header("Authorization")] string authorization,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Extension methods for ACE Hub API client
/// </summary>
public static class AceHubApiClientExtensions
{
    /// <summary>
    /// Helper method to get rate with automatic token management
    /// </summary>
    public static async Task<AceHubRateResponse> GetRateWithAuthAsync(
        this IAceHubApiClient client,
        AceHubRateRequest request,
        string clientId,
        string clientSecret,
        CancellationToken cancellationToken = default)
    {
        // Get token
        var tokenRequest = new AceHubTokenRequest
        {
            ClientId = clientId,
            ClientSecret = clientSecret
        };

        var tokenResponse = await client.GetTokenAsync(tokenRequest, cancellationToken);

        // Make rate call with token
        var authorization = $"Bearer {tokenResponse.AccessToken}";
        return await client.GetRateAsync(request, authorization, cancellationToken);
    }

    /// <summary>
    /// Helper method to get document with automatic token management
    /// </summary>
    public static async Task<AceHubDocumentResponse> GetDocumentWithAuthAsync(
        this IAceHubApiClient client,
        AceHubDocumentRequest request,
        string clientId,
        string clientSecret,
        CancellationToken cancellationToken = default)
    {
        // Get token
        var tokenRequest = new AceHubTokenRequest
        {
            ClientId = clientId,
            ClientSecret = clientSecret
        };

        var tokenResponse = await client.GetTokenAsync(tokenRequest, cancellationToken);

        // Make document call with token
        var authorization = $"Bearer {tokenResponse.AccessToken}";
        return await client.GetDocumentAsync(request, authorization, cancellationToken);
    }
}
