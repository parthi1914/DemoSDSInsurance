using InsuranceWrapperApi.Application.DTOs.Common;

namespace InsuranceWrapperApi.Application.DTOs.LOB.Flood;

/// <summary>
/// Flood insurance specific DTO
/// </summary>
public class FloodQuoteRequest
{
    public string PropertyOwnerName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Address PropertyAddress { get; set; } = new();
    public DateTime EffectiveDate { get; set; }
    
    // Flood Specific
    public string FloodZone { get; set; } = string.Empty; // A, AE, X, etc.
    public string CommunityNumber { get; set; } = string.Empty;
    public string PanelNumber { get; set; } = string.Empty;
    public DateTime? FIRMDate { get; set; } // Flood Insurance Rate Map date
    
    // Building Information
    public string OccupancyType { get; set; } = string.Empty; // Single-family, Condo, etc.
    public string FoundationType { get; set; } = string.Empty; // Basement, Crawl, Slab, etc.
    public int NumberOfFloors { get; set; }
    public int YearBuilt { get; set; }
    public bool HasBasement { get; set; }
    public bool HasEnclosedArea { get; set; }
    
    // Elevation Information
    public bool HasElevationCertificate { get; set; }
    public decimal? LowestFloorElevation { get; set; }
    public decimal? BaseFloodElevation { get; set; }
    public string? ElevationDifference { get; set; }
    public DateTime? ElevationCertificateDate { get; set; }
    
    // Coverage
    public decimal BuildingCoverageAmount { get; set; }
    public decimal ContentsCoverageAmount { get; set; }
    public decimal BuildingDeductible { get; set; }
    public decimal ContentsDeductible { get; set; }
    
    // Additional
    public bool IsPreferredRiskPolicy { get; set; }
    public bool IsCondo { get; set; }
    public bool IsNonProfit { get; set; }
    public string? MortgageeName { get; set; }
    public string? MortgageeAddress { get; set; }
    
    public Dictionary<string, object>? AdditionalInfo { get; set; }
}

public class FloodBindRequest
{
    public string QuoteId { get; set; } = string.Empty;
    public PaymentInfo Payment { get; set; } = new();
    public Dictionary<string, object>? AdditionalBindInfo { get; set; }
}
