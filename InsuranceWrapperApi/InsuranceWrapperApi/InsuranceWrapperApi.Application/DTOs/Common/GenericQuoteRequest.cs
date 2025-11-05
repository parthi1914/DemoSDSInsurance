using InsuranceWrapperApi.Domain.Enums;

namespace InsuranceWrapperApi.Application.DTOs.Common;

/// <summary>
/// Generic request DTO that accepts basic user input
/// System will auto-detect the LOB and provider based on this input
/// </summary>
public class GenericQuoteRequest
{
    // Common fields
    public string? BusinessName { get; set; }
    public string? ContactName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public Address? BusinessAddress { get; set; }
    public DateTime EffectiveDate { get; set; }
    public string? IndustryType { get; set; }
    public int? YearsInBusiness { get; set; }
    
    // GL Indicators
    public string? GLClassCode { get; set; }
    public string? OperationsDescription { get; set; }
    public decimal? AnnualRevenue { get; set; }
    public int? NumberOfEmployees { get; set; }
    
    // Property Indicators
    public decimal? BuildingValue { get; set; }
    public decimal? ContentsValue { get; set; }
    public string? ConstructionType { get; set; }
    public int? YearBuilt { get; set; }
    public bool? HasSprinklers { get; set; }
    public bool? HasAlarm { get; set; }
    
    // Flood Indicators
    public string? FloodZone { get; set; }
    public bool? HasElevationCertificate { get; set; }
    public decimal? BaseFloodElevation { get; set; }
    public string? BuildingOccupancyType { get; set; }
    
    // Worker Compensation Indicators
    public List<PayrollInfo>? PayrollByClass { get; set; }
    public string? StateOfOperation { get; set; }
    public bool? HasPriorClaims { get; set; }
    
    // Optional: Force specific provider
    public ProviderType? PreferredProvider { get; set; }
    
    // Optional: Additional metadata
    public Dictionary<string, object>? AdditionalData { get; set; }
}

public class Address
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? County { get; set; }
}

public class PayrollInfo
{
    public string? ClassCode { get; set; }
    public string? ClassDescription { get; set; }
    public decimal AnnualPayroll { get; set; }
    public int NumberOfEmployees { get; set; }
}
