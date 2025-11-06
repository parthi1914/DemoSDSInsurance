using System.Text.Json.Serialization;

namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad;

/// <summary>
/// General Liability Line of Business
/// </summary>
public class GeneralLiabilityLineBusiness
{
    [JsonPropertyName("CurrentTermAmt")]
    public CurrentTermAmt CurrentTermAmt { get; set; } = new();

    [JsonPropertyName("IRH_RatedPremium")]
    public decimal IRH_RatedPremium { get; set; }

    [JsonPropertyName("MinPremInd")]
    public string MinPremInd { get; set; } = "No";

    [JsonPropertyName("MinPremAmt")]
    public MinPremAmt MinPremAmt { get; set; } = new();

    [JsonPropertyName("LiabilityInfo")]
    public LiabilityInfo LiabilityInfo { get; set; } = new();

    [JsonPropertyName("Deductible")]
    public List<Deductible> Deductible { get; set; } = new();

    [JsonPropertyName("OptionalCoverageList")]
    public OptionalCoverageList OptionalCoverageList { get; set; } = new();

    [JsonPropertyName("AdditionalInsuredList")]
    public AdditionalInsuredList AdditionalInsuredList { get; set; } = new();
}

public class CurrentTermAmt
{
    [JsonPropertyName("Amt")]
    public string Amt { get; set; } = string.Empty;
}

public class MinPremAmt
{
    [JsonPropertyName("Amt")]
    public decimal Amt { get; set; }
}

/// <summary>
/// Liability Information with Coverages and Classifications
/// </summary>
public class LiabilityInfo
{
    [JsonPropertyName("Coverage")]
    public List<GLCoverage> Coverage { get; set; } = new();

    [JsonPropertyName("GeneralLiabilityClassification")]
    public List<GeneralLiabilityClassification> GeneralLiabilityClassification { get; set; } = new();

    [JsonPropertyName("GLCarrierSpecified")]
    public GLCarrierSpecified GLCarrierSpecified { get; set; } = new();
}

/// <summary>
/// GL Coverage (General Aggregate, Each Occurrence, etc.)
/// </summary>
public class GLCoverage
{
    /// <summary>
    /// GENAG, EAOCC, PRDCO, PIADV, MEDEX, PROF, FIRDM
    /// </summary>
    [JsonPropertyName("CoverageCd")]
    public string CoverageCd { get; set; } = string.Empty;

    [JsonPropertyName("CoverageDesc")]
    public string CoverageDesc { get; set; } = string.Empty;

    [JsonPropertyName("Limit")]
    public List<Limit> Limit { get; set; } = new();
}

public class Limit
{
    [JsonPropertyName("FormatCurrencyAmt")]
    public FormatCurrencyAmt FormatCurrencyAmt { get; set; } = new();

    /// <summary>
    /// Aggregate, PerOcc, Excl
    /// </summary>
    [JsonPropertyName("LimitAppliesToCd")]
    public string LimitAppliesToCd { get; set; } = string.Empty;

    [JsonPropertyName("ValuationCd")]
    public string? ValuationCd { get; set; }
}

public class FormatCurrencyAmt
{
    [JsonPropertyName("Amt")]
    public string Amt { get; set; } = string.Empty;
}

/// <summary>
/// General Liability Classification (Class Codes)
/// </summary>
public class GeneralLiabilityClassification
{
    [JsonPropertyName("Coverage")]
    public List<ClassificationCoverage> Coverage { get; set; } = new();

    /// <summary>
    /// GL Class Code (e.g., 60010 for Apartments, 91342 for Carpentry)
    /// </summary>
    [JsonPropertyName("ClassCd")]
    public string ClassCd { get; set; } = string.Empty;

    [JsonPropertyName("ClassCdDesc")]
    public string ClassCdDesc { get; set; } = string.Empty;

    [JsonPropertyName("IRH_SubClassCd")]
    public string IRH_SubClassCd { get; set; } = string.Empty;

    [JsonPropertyName("IRH_SubClassCdDesc")]
    public string IRH_SubClassCdDesc { get; set; } = string.Empty;

    [JsonPropertyName("IRH_DaggerClass")]
    public string IRH_DaggerClass { get; set; } = "No";

    /// <summary>
    /// The exposure base (revenue, payroll, units, etc.)
    /// </summary>
    [JsonPropertyName("Exposure")]
    public decimal Exposure { get; set; }

    [JsonPropertyName("NumEmployeesPartTime")]
    public int NumEmployeesPartTime { get; set; }

    [JsonPropertyName("NumEmployeesFullTime")]
    public int NumEmployeesFullTime { get; set; }

    [JsonPropertyName("comIRH_IsCalculateStatePayroll")]
    public bool ComIRH_IsCalculateStatePayroll { get; set; }

    [JsonPropertyName("IRH_PayrollType")]
    public string? IRH_PayrollType { get; set; }

    [JsonPropertyName("comIRH_NoOfOwners")]
    public int ComIRH_NoOfOwners { get; set; }

    [JsonPropertyName("comIRH_PayrollPercentage")]
    public decimal ComIRH_PayrollPercentage { get; set; }

    [JsonPropertyName("comIRH_EmployeePayroll")]
    public decimal ComIRH_EmployeePayroll { get; set; }

    /// <summary>
    /// PAYRL (Payroll), Unit, SALES (Sales), etc.
    /// </summary>
    [JsonPropertyName("PremiumBasisCd")]
    public string PremiumBasisCd { get; set; } = string.Empty;

    [JsonPropertyName("PremiumBasisDesc")]
    public string PremiumBasisDesc { get; set; } = string.Empty;

    [JsonPropertyName("IfAnyRatingBasisInd")]
    public string IfAnyRatingBasisInd { get; set; } = "false";

    [JsonPropertyName("CreditOrSurcharge")]
    public decimal CreditOrSurcharge { get; set; } = 1;

    [JsonPropertyName("LocationRef")]
    public string LocationRef { get; set; } = string.Empty;

    [JsonPropertyName("IRH_IsMaxRevenueGoverningClass")]
    public bool IRH_IsMaxRevenueGoverningClass { get; set; }
}

public class ClassificationCoverage
{
    /// <summary>
    /// PREM (Premises), PRDCO (Products & Completed Operations)
    /// </summary>
    [JsonPropertyName("CoverageCd")]
    public string CoverageCd { get; set; } = string.Empty;

    [JsonPropertyName("CoverageDesc")]
    public string CoverageDesc { get; set; } = string.Empty;
}

/// <summary>
/// GL Carrier Specific Questions
/// </summary>
public class GLCarrierSpecified
{
    [JsonPropertyName("IsOwnSecurityProvided")]
    public string IsOwnSecurityProvided { get; set; } = "false";

    [JsonPropertyName("IsSnowShovelingContractedOut")]
    public string IsSnowShovelingContractedOut { get; set; } = "false";

    [JsonPropertyName("IsAssaultAndBatteryExclusion")]
    public string IsAssaultAndBatteryExclusion { get; set; } = "true";

    [JsonPropertyName("IsTotalFirearmExclusion")]
    public string IsTotalFirearmExclusion { get; set; } = "true";
}

/// <summary>
/// Deductible Information
/// </summary>
public class Deductible
{
    [JsonPropertyName("FormatInteger")]
    public int FormatInteger { get; set; }

    [JsonPropertyName("FormatPct")]
    public decimal FormatPct { get; set; }

    /// <summary>
    /// CL (Claim), AOP (All Other Perils)
    /// </summary>
    [JsonPropertyName("DeductibleTypeCd")]
    public string DeductibleTypeCd { get; set; } = string.Empty;

    /// <summary>
    /// BIPD (Bodily Injury Property Damage), AllPeril, Wind, etc.
    /// </summary>
    [JsonPropertyName("DeductibleAppliesToCd")]
    public string DeductibleAppliesToCd { get; set; } = string.Empty;
}

public class OptionalCoverageList
{
    [JsonPropertyName("CoverageDetail")]
    public List<object> CoverageDetail { get; set; } = new();
}

public class AdditionalInsuredList
{
    [JsonPropertyName("CoverageDetail")]
    public List<object> CoverageDetail { get; set; } = new();
}
