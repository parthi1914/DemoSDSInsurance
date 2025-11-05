using InsuranceWrapperApi.Application.DTOs.Common;

namespace InsuranceWrapperApi.Application.DTOs.LOB.Property;

/// <summary>
/// Property insurance specific DTO
/// </summary>
public class PropertyQuoteRequest
{
    public string BusinessName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Address PropertyAddress { get; set; } = new();
    public DateTime EffectiveDate { get; set; }
    
    // Building Information
    public decimal BuildingValue { get; set; }
    public decimal BusinessPersonalPropertyValue { get; set; }
    public int YearBuilt { get; set; }
    public int NumberOfStories { get; set; }
    public int TotalSquareFootage { get; set; }
    public string ConstructionType { get; set; } = string.Empty; // Frame, Masonry, etc.
    public string RoofType { get; set; } = string.Empty;
    public int RoofAge { get; set; }
    public string OccupancyType { get; set; } = string.Empty;
    
    // Protection
    public bool HasSprinklerSystem { get; set; }
    public string? SprinklerType { get; set; }
    public bool HasFireAlarm { get; set; }
    public bool HasBurglarAlarm { get; set; }
    public bool HasSecurityGuard { get; set; }
    public int DistanceToFireStation { get; set; }
    public int DistanceToHydrant { get; set; }
    public string FireProtectionClass { get; set; } = string.Empty;
    
    // Coverage Details
    public string ValuationType { get; set; } = "ReplacementCost"; // RC or ACV
    public decimal BuildingDeductible { get; set; }
    public decimal BPPDeductible { get; set; }
    public bool BusinessIncomeNeeded { get; set; }
    public decimal? BusinessIncomeLimit { get; set; }
    public bool EquipmentBreakdownNeeded { get; set; }
    
    // Loss History
    public bool HasPriorClaims { get; set; }
    public List<PropertyClaim>? PriorClaims { get; set; }
    
    public Dictionary<string, object>? AdditionalInfo { get; set; }
}

public class PropertyClaim
{
    public DateTime DateOfLoss { get; set; }
    public string? LossType { get; set; } // Fire, Water, Wind, Theft
    public decimal AmountPaid { get; set; }
    public string? Description { get; set; }
}

public class PropertyBindRequest
{
    public string QuoteId { get; set; } = string.Empty;
    public PaymentInfo Payment { get; set; } = new();
    public Dictionary<string, object>? AdditionalBindInfo { get; set; }
}
