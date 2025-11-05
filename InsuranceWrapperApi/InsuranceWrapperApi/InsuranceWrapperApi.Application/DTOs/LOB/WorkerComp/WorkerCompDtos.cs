using InsuranceWrapperApi.Application.DTOs.Common;

namespace InsuranceWrapperApi.Application.DTOs.LOB.WorkerComp;

/// <summary>
/// Worker Compensation specific DTO
/// </summary>
public class WorkerCompQuoteRequest
{
    public string BusinessName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Address BusinessAddress { get; set; } = new();
    public DateTime EffectiveDate { get; set; }
    
    // Business Information
    public string FederalEmployerID { get; set; } = string.Empty;
    public string StateOfOperation { get; set; } = string.Empty;
    public int YearsInBusiness { get; set; }
    public string LegalEntity { get; set; } = string.Empty; // Corp, LLC, Sole Proprietor
    
    // Payroll and Employee Information
    public List<WCPayrollClass> PayrollByClass { get; set; } = new();
    public int TotalFullTimeEmployees { get; set; }
    public int TotalPartTimeEmployees { get; set; }
    public decimal TotalAnnualPayroll { get; set; }
    
    // Owner/Officer Information
    public List<WCOwner>? Owners { get; set; }
    public bool OwnersIncludedInPayroll { get; set; }
    
    // Experience Modification
    public bool HasExperienceMod { get; set; }
    public decimal? CurrentExperienceMod { get; set; }
    public string? ExperienceModState { get; set; }
    
    // Loss History
    public bool HasPriorClaims { get; set; }
    public List<WCClaim>? PriorClaims { get; set; }
    
    // Additional Coverage
    public bool EmployersLiabilityNeeded { get; set; }
    public decimal? ELEachAccidentLimit { get; set; }
    public decimal? ELDiseaseEachEmployeeLimit { get; set; }
    public decimal? ELDiseasePolicyLimit { get; set; }
    
    // Subcontractors
    public bool UsesSubcontractors { get; set; }
    public decimal? AnnualSubcontractorCosts { get; set; }
    public bool SubcontractorsHaveOwnWC { get; set; }
    
    public Dictionary<string, object>? AdditionalInfo { get; set; }
}

public class WCPayrollClass
{
    public string ClassCode { get; set; } = string.Empty;
    public string ClassDescription { get; set; } = string.Empty;
    public string StateCode { get; set; } = string.Empty;
    public decimal AnnualPayroll { get; set; }
    public int NumberOfEmployees { get; set; }
    public decimal? EstimatedRate { get; set; } // Optional rate per $100 of payroll
}

public class WCOwner
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal OwnershipPercentage { get; set; }
    public bool IncludedInPayroll { get; set; }
    public bool ExcludedFromCoverage { get; set; }
    public string? ClassCode { get; set; }
}

public class WCClaim
{
    public DateTime DateOfInjury { get; set; }
    public string? InjuryType { get; set; }
    public string? BodyPart { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal AmountReserved { get; set; }
    public string? ClaimStatus { get; set; }
    public string? Description { get; set; }
}

public class WorkerCompBindRequest
{
    public string QuoteId { get; set; } = string.Empty;
    public PaymentInfo Payment { get; set; } = new();
    public Dictionary<string, object>? AdditionalBindInfo { get; set; }
}
