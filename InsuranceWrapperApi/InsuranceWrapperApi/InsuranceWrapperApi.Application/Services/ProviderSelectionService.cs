using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.Interfaces;
using InsuranceWrapperApi.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace InsuranceWrapperApi.Application.Services;

/// <summary>
/// Service to select appropriate provider based on LOB and business rules
/// </summary>
public class ProviderSelectionService : IProviderSelectionService
{
    private readonly IConfiguration _configuration;

    public ProviderSelectionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ProviderType SelectProvider(LineOfBusiness lob, GenericQuoteRequest request)
    {
        // If user has specified a preferred provider, use that
        if (request.PreferredProvider.HasValue && request.PreferredProvider.Value != ProviderType.Unknown)
        {
            return request.PreferredProvider.Value;
        }

        // Get default provider from configuration
        var defaultProvider = _configuration[$"Providers:Default:{lob}"];
        if (!string.IsNullOrWhiteSpace(defaultProvider) && Enum.TryParse<ProviderType>(defaultProvider, out var provider))
        {
            return provider;
        }

        // Apply business rules to select provider
        return ApplyBusinessRules(lob, request);
    }

    private ProviderType ApplyBusinessRules(LineOfBusiness lob, GenericQuoteRequest request)
    {
        // Example business rules (customize based on your requirements)
        
        return lob switch
        {
            LineOfBusiness.GeneralLiability => SelectGLProvider(request),
            LineOfBusiness.Property => SelectPropertyProvider(request),
            LineOfBusiness.Flood => SelectFloodProvider(request),
            LineOfBusiness.WorkerCompensation => SelectWCProvider(request),
            _ => ProviderType.Dyad // Default fallback
        };
    }

    private ProviderType SelectGLProvider(GenericQuoteRequest request)
    {
        // Example: Route high-revenue businesses to Dyad
        if (request.AnnualRevenue.HasValue && request.AnnualRevenue.Value > 5_000_000)
        {
            return ProviderType.Dyad;
        }

        // Route construction-related to Herald
        if (request.IndustryType?.Contains("Construction", StringComparison.OrdinalIgnoreCase) == true)
        {
            return ProviderType.Herald;
        }

        // Default to Zywave
        return ProviderType.Zywave;
    }

    private ProviderType SelectPropertyProvider(GenericQuoteRequest request)
    {
        // Example: High-value properties to Dyad
        if (request.BuildingValue.HasValue && request.BuildingValue.Value > 2_000_000)
        {
            return ProviderType.Dyad;
        }

        // Default to Herald
        return ProviderType.Herald;
    }

    private ProviderType SelectFloodProvider(GenericQuoteRequest request)
    {
        // Example: High-risk flood zones to specialized provider
        if (request.FloodZone?.StartsWith("A") == true || request.FloodZone?.StartsWith("V") == true)
        {
            return ProviderType.Zywave;
        }

        // Default to Dyad
        return ProviderType.Dyad;
    }

    private ProviderType SelectWCProvider(GenericQuoteRequest request)
    {
        // Example: Large payrolls to Herald
        var totalPayroll = request.PayrollByClass?.Sum(p => p.AnnualPayroll) ?? 0;
        if (totalPayroll > 2_000_000)
        {
            return ProviderType.Herald;
        }

        // Default to Zywave
        return ProviderType.Zywave;
    }
}
