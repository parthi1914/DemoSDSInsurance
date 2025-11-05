using InsuranceWrapperApi.Domain.Enums;

namespace InsuranceWrapperApi.Application.DTOs.Common;

/// <summary>
/// Generic quote response returned to user
/// </summary>
public class GenericQuoteResponse
{
    public bool Success { get; set; }
    public string? QuoteId { get; set; }
    public LineOfBusiness LineOfBusiness { get; set; }
    public ProviderType Provider { get; set; }
    public decimal? Premium { get; set; }
    public decimal? Fees { get; set; }
    public decimal? TotalCost { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public List<Coverage>? Coverages { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, object>? ProviderSpecificData { get; set; }
    public List<string>? Errors { get; set; }
}

/// <summary>
/// Generic bind response returned to user
/// </summary>
public class GenericBindResponse
{
    public bool Success { get; set; }
    public string? PolicyNumber { get; set; }
    public string? QuoteId { get; set; }
    public LineOfBusiness LineOfBusiness { get; set; }
    public ProviderType Provider { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public decimal? BoundPremium { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, object>? ProviderSpecificData { get; set; }
    public List<string>? Errors { get; set; }
}

public class Coverage
{
    public string? CoverageType { get; set; }
    public decimal? Limit { get; set; }
    public decimal? Deductible { get; set; }
    public decimal? Premium { get; set; }
}
