using Refit;
using InsuranceWrapperApi.Application.DTOs.Providers.Dyad;
using InsuranceWrapperApi.Application.DTOs.Providers.Herald;
using InsuranceWrapperApi.Application.DTOs.Providers.Zywave;

namespace InsuranceWrapperApi.Infrastructure.ApiClients;

/// <summary>
/// Dyad API client using Refit
/// </summary>
public interface IDyadApiClient
{
    [Post("/api/v1/quote")]
    Task<DyadQuoteResponse> GetQuoteAsync([Body] DyadQuoteRequest request, CancellationToken cancellationToken = default);

    [Post("/api/v1/bind")]
    Task<DyadBindResponse> BindPolicyAsync([Body] DyadBindRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Herald API client using Refit
/// </summary>
public interface IHeraldApiClient
{
    [Post("/v2/insurance/quote")]
    Task<HeraldQuoteResponse> GetQuoteAsync([Body] HeraldQuoteRequest request, CancellationToken cancellationToken = default);

    [Post("/v2/insurance/bind")]
    Task<HeraldBindResponse> BindPolicyAsync([Body] HeraldBindRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Zywave API client using Refit
/// </summary>
public interface IZywaveApiClient
{
    [Post("/transactions/quote")]
    Task<ZywaveQuoteResponse> GetQuoteAsync([Body] ZywaveQuoteRequest request, CancellationToken cancellationToken = default);

    [Post("/transactions/bind")]
    Task<ZywaveBindResponse> BindPolicyAsync([Body] ZywaveBindRequest request, CancellationToken cancellationToken = default);
}
