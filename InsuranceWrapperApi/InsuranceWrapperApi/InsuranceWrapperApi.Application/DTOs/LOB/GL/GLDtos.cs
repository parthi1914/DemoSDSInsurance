using InsuranceWrapperApi.Application.DTOs.Common;

namespace InsuranceWrapperApi.Application.DTOs.LOB.GL;

/// <summary>
/// General Liability specific DTO
/// </summary>
public class GLQuoteRequest
{
    public string BusinessName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Address BusinessAddress { get; set; } = new();
    public DateTime EffectiveDate { get; set; }
    
    // GL Specific
    public string ClassCode { get; set; } = string.Empty;
    public string ClassDescription { get; set; } = string.Empty;
    public string OperationsDescription { get; set; } = string.Empty;
    public decimal AnnualRevenue { get; set; }
    public int NumberOfEmployees { get; set; }
    public int YearsInBusiness { get; set; }
    
    // Coverage Details
    public decimal GeneralAggregateLimitRequested { get; set; } = 2_000_000;
    public decimal PerOccurrenceLimitRequested { get; set; } = 1_000_000;
    public decimal PersonalAndAdvInjuryLimitRequested { get; set; } = 1_000_000;
    public decimal ProductsCompletedOpsAggregateRequested { get; set; } = 2_000_000;
    public decimal MedicalExpenseLimitRequested { get; set; } = 5_000;
    public decimal DamageToRentedPremisesRequested { get; set; } = 100_000;
    
    // Loss History
    public bool HasPriorClaims { get; set; }
    public List<GLClaim>? PriorClaims { get; set; }
    
    // Additional Coverages
    public bool LiquorLiabilityNeeded { get; set; }
    public bool EmploymentPracticesLiabilityNeeded { get; set; }
    public bool CyberLiabilityNeeded { get; set; }
    
    public Dictionary<string, object>? AdditionalInfo { get; set; }
}

public class GLClaim
{
    public DateTime DateOfLoss { get; set; }
    public string? ClaimType { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal AmountReserved { get; set; }
    public string? Description { get; set; }
}

public class GLBindRequest
{
    public string QuoteId { get; set; } = string.Empty;
    public PaymentInfo Payment { get; set; } = new();
    public Dictionary<string, object>? AdditionalBindInfo { get; set; }
}
