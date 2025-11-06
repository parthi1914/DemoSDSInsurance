using System.Text.Json.Serialization;

namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad;

/// <summary>
/// Commercial Property Line of Business
/// </summary>
public class CommlPropertyLineBusiness
{
    [JsonPropertyName("PropertyInfo")]
    public PropertyInfo PropertyInfo { get; set; } = new();
}

public class PropertyInfo
{
    [JsonPropertyName("CreditOrSurcharge")]
    public decimal CreditOrSurcharge { get; set; }

    [JsonPropertyName("CommlPropertyInfo")]
    public List<CommlPropertyInfo> CommlPropertyInfo { get; set; } = new();

    [JsonPropertyName("PropCarrierSpecifiedPolicyInfo")]
    public PropCarrierSpecifiedPolicyInfo PropCarrierSpecifiedPolicyInfo { get; set; } = new();
}

/// <summary>
/// Commercial Property Information
/// </summary>
public class CommlPropertyInfo
{
    /// <summary>
    /// Property Class Code (e.g., 0311 for Apartments)
    /// </summary>
    [JsonPropertyName("ClassCd")]
    public string ClassCd { get; set; } = string.Empty;

    [JsonPropertyName("ClassCdDesc")]
    public string ClassCdDesc { get; set; } = string.Empty;

    [JsonPropertyName("comIRH_CSPCdExt")]
    public string? ComIRH_CSPCdExt { get; set; }

    [JsonPropertyName("Coverage")]
    public List<PropertyCoverage> Coverage { get; set; } = new();

    [JsonPropertyName("Rate")]
    public decimal Rate { get; set; }

    [JsonPropertyName("CurrentTermAmt")]
    public CurrentTermAmt? CurrentTermAmt { get; set; }

    [JsonPropertyName("IRH_RatedPremium")]
    public decimal IRH_RatedPremium { get; set; }

    [JsonPropertyName("LocationRef")]
    public int LocationRef { get; set; }

    [JsonPropertyName("SubLocationRef")]
    public int SubLocationRef { get; set; }

    [JsonPropertyName("PropCarrierSpecified")]
    public PropCarrierSpecified PropCarrierSpecified { get; set; } = new();

    [JsonPropertyName("RiskAppetiteCd")]
    public string? RiskAppetiteCd { get; set; }
}

/// <summary>
/// Property Coverage (Building, BPP, etc.)
/// </summary>
public class PropertyCoverage
{
    /// <summary>
    /// BLDG (Building), BPP (Business Personal Property), BI (Business Income), etc.
    /// </summary>
    [JsonPropertyName("CoverageCd")]
    public string CoverageCd { get; set; } = string.Empty;

    [JsonPropertyName("CoverageDesc")]
    public string CoverageDesc { get; set; } = string.Empty;

    [JsonPropertyName("Limit")]
    public List<PropertyLimit> Limit { get; set; } = new();

    [JsonPropertyName("Deductible")]
    public List<Deductible> Deductible { get; set; } = new();

    [JsonPropertyName("CommlCoverageSupplement")]
    public CommlCoverageSupplement CommlCoverageSupplement { get; set; } = new();

    [JsonPropertyName("CurrentTermAmt")]
    public CurrentTermAmt CurrentTermAmt { get; set; } = new();

    [JsonPropertyName("Option")]
    public List<CoverageOption> Option { get; set; } = new();

    [JsonPropertyName("CreditOrSurcharge")]
    public CreditOrSurcharge CreditOrSurcharge { get; set; } = new();

    [JsonPropertyName("Rate")]
    public string Rate { get; set; } = string.Empty;

    [JsonPropertyName("MinPremAmt")]
    public MinPremAmt MinPremAmt { get; set; } = new();

    [JsonPropertyName("AddlCoverage")]
    public List<object> AddlCoverage { get; set; } = new();

    [JsonPropertyName("CreditOrSurchargeInfo")]
    public List<object> CreditOrSurchargeInfo { get; set; } = new();
}

public class PropertyLimit
{
    [JsonPropertyName("FormatCurrencyAmt")]
    public FormatCurrencyAmt FormatCurrencyAmt { get; set; } = new();

    [JsonPropertyName("LimitAppliesToCd")]
    public string LimitAppliesToCd { get; set; } = string.Empty;

    /// <summary>
    /// ACV (Actual Cash Value), RC (Replacement Cost)
    /// </summary>
    [JsonPropertyName("ValuationCd")]
    public string ValuationCd { get; set; } = "ACV";
}

/// <summary>
/// Commercial Coverage Supplement
/// </summary>
public class CommlCoverageSupplement
{
    /// <summary>
    /// BASIC, BROAD, SPECIAL
    /// </summary>
    [JsonPropertyName("CoverageSubCd")]
    public string CoverageSubCd { get; set; } = "BASIC";

    /// <summary>
    /// Coinsurance Percentage: 80, 90, 100
    /// </summary>
    [JsonPropertyName("CoinsurancePct")]
    public string CoinsurancePct { get; set; } = "80";

    [JsonPropertyName("ClaimMadeInfo")]
    public ClaimMadeInfo ClaimMadeInfo { get; set; } = new();
}

public class ClaimMadeInfo
{
    [JsonPropertyName("UnlimitedPriorActsCoverageInd")]
    public bool UnlimitedPriorActsCoverageInd { get; set; }

    [JsonPropertyName("CurrentRetroactiveDt")]
    public DateTime? CurrentRetroactiveDt { get; set; }

    [JsonPropertyName("RetroactiveCoverageInd")]
    public bool RetroactiveCoverageInd { get; set; }
}

/// <summary>
/// Coverage Option (e.g., Wind Inclusion/Exclusion)
/// </summary>
public class CoverageOption
{
    /// <summary>
    /// Wind, Flood, Earthquake, etc.
    /// </summary>
    [JsonPropertyName("OptionTypeCd")]
    public string OptionTypeCd { get; set; } = string.Empty;

    /// <summary>
    /// Incl (Included), Excl (Excluded)
    /// </summary>
    [JsonPropertyName("OptionValue")]
    public string OptionValue { get; set; } = string.Empty;

    [JsonPropertyName("OptionValueDesc")]
    public string OptionValueDesc { get; set; } = string.Empty;

    [JsonPropertyName("LocationNo")]
    public string LocationNo { get; set; } = string.Empty;

    [JsonPropertyName("BuildingNo")]
    public string BuildingNo { get; set; } = string.Empty;
}

public class CreditOrSurcharge
{
    [JsonPropertyName("CreditSurchargeCd")]
    public string? CreditSurchargeCd { get; set; }

    [JsonPropertyName("NumericValue")]
    public NumericValue NumericValue { get; set; } = new();
}

public class NumericValue
{
    [JsonPropertyName("FormatModFactor")]
    public decimal FormatModFactor { get; set; }
}

/// <summary>
/// Property Carrier Specific Information
/// </summary>
public class PropCarrierSpecified
{
    [JsonPropertyName("SquareFootagePercentageOccupiedByInsured")]
    public string SquareFootagePercentageOccupiedByInsured { get; set; } = "0";

    [JsonPropertyName("IsOwnedByInsured")]
    public string IsOwnedByInsured { get; set; } = "true";

    [JsonPropertyName("RoofShapeType")]
    public string RoofShapeType { get; set; } = string.Empty;

    [JsonPropertyName("RoofConstructionType")]
    public string RoofConstructionType { get; set; } = string.Empty;

    [JsonPropertyName("IsHistoricalRegistry")]
    public string IsHistoricalRegistry { get; set; } = "false";

    [JsonPropertyName("IsTemporaryOrIncidental")]
    public string IsTemporaryOrIncidental { get; set; } = "false";

    [JsonPropertyName("IsPlumbingTypeInListOfIneligibleTypes")]
    public string IsPlumbingTypeInListOfIneligibleTypes { get; set; } = "false";

    [JsonPropertyName("IsHeatingTypeInListOfIneligibleTypes")]
    public string IsHeatingTypeInListOfIneligibleTypes { get; set; } = "false";

    [JsonPropertyName("IsWiringTypeInListOfIneligibleTypes")]
    public string IsWiringTypeInListOfIneligibleTypes { get; set; } = "false";

    [JsonPropertyName("IsElectricCircuitBreakerProtection")]
    public string IsElectricCircuitBreakerProtection { get; set; } = "false";

    [JsonPropertyName("IsOperatingBusinessOpenPastMidnight")]
    public string IsOperatingBusinessOpenPastMidnight { get; set; } = "false";

    [JsonPropertyName("NumberOfUnitsOccupyTenant")]
    public string NumberOfUnitsOccupyTenant { get; set; } = "0";

    [JsonPropertyName("PercentageOccupiedByInsured")]
    public string PercentageOccupiedByInsured { get; set; } = "0.00";

    [JsonPropertyName("PercentageVacantByInsured")]
    public string PercentageVacantByInsured { get; set; } = string.Empty;
}

/// <summary>
/// Property Carrier Specified Policy Information
/// </summary>
public class PropCarrierSpecifiedPolicyInfo
{
    [JsonPropertyName("AccountsReceivableLimitAmount")]
    public string AccountsReceivableLimitAmount { get; set; } = "10000";

    [JsonPropertyName("WaterSewageBackupLimitAmount")]
    public string WaterSewageBackupLimitAmount { get; set; } = "10000";

    [JsonPropertyName("FireDepartmentServiceChargeLimitAmount")]
    public string FireDepartmentServiceChargeLimitAmount { get; set; } = "5000";

    [JsonPropertyName("FireProtectionDeviceRechargeLimitAmount")]
    public string FireProtectionDeviceRechargeLimitAmount { get; set; } = "2500";

    [JsonPropertyName("ValuablePapersAndRecordsLimitAmount")]
    public string ValuablePapersAndRecordsLimitAmount { get; set; } = "10000";

    [JsonPropertyName("PolicyLevelOptionalCoveragePremiumTotal")]
    public string PolicyLevelOptionalCoveragePremiumTotal { get; set; } = string.Empty;
}
