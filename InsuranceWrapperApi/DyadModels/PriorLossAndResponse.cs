using System.Text.Json.Serialization;

namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad;

/// <summary>
/// Prior Loss Information
/// </summary>
public class PriorLoss
{
    [JsonPropertyName("PriorLossHistoryInfo")]
    public List<PriorLossHistoryInfo> PriorLossHistoryInfo { get; set; } = new();
}

public class PriorLossHistoryInfo
{
    [JsonPropertyName("LossDt")]
    public DateTime LossDt { get; set; }

    [JsonPropertyName("LossDesc")]
    public string LossDesc { get; set; } = string.Empty;

    [JsonPropertyName("LossTypeCd")]
    public string LossTypeCd { get; set; } = string.Empty;

    [JsonPropertyName("LossAmt")]
    public decimal LossAmt { get; set; }

    [JsonPropertyName("ClaimStatusCd")]
    public string ClaimStatusCd { get; set; } = string.Empty;
}

/// <summary>
/// ACE Hub Rate Response
/// </summary>
public class AceHubRateResponse
{
    [JsonPropertyName("InsuranceSvcRs")]
    public InsuranceSvcRs? InsuranceSvcRs { get; set; }

    [JsonPropertyName("Status")]
    public ResponseStatus? Status { get; set; }
}

public class InsuranceSvcRs
{
    [JsonPropertyName("RqUID")]
    public string RqUID { get; set; } = string.Empty;

    [JsonPropertyName("IRH_QuoteNo")]
    public string IRH_QuoteNo { get; set; } = string.Empty;

    [JsonPropertyName("CommlPkgPolicyQuoteInqRs")]
    public CommlPkgPolicyQuoteInqRs? CommlPkgPolicyQuoteInqRs { get; set; }

    [JsonPropertyName("Carriers")]
    public List<CarrierResponse>? Carriers { get; set; }
}

public class CommlPkgPolicyQuoteInqRs
{
    [JsonPropertyName("GeneralLiabilityLineBusiness")]
    public GLResponseBusiness? GeneralLiabilityLineBusiness { get; set; }

    [JsonPropertyName("CommlPropertyLineBusiness")]
    public PropertyResponseBusiness? CommlPropertyLineBusiness { get; set; }

    [JsonPropertyName("PolicySummaryInfo")]
    public PolicySummaryInfo? PolicySummaryInfo { get; set; }
}

public class GLResponseBusiness
{
    [JsonPropertyName("CurrentTermAmt")]
    public ResponseAmount? CurrentTermAmt { get; set; }

    [JsonPropertyName("LiabilityInfo")]
    public LiabilityResponseInfo? LiabilityInfo { get; set; }
}

public class PropertyResponseBusiness
{
    [JsonPropertyName("PropertyInfo")]
    public PropertyResponseInfo? PropertyInfo { get; set; }
}

public class LiabilityResponseInfo
{
    [JsonPropertyName("GeneralLiabilityClassification")]
    public List<GLClassificationResponse>? GeneralLiabilityClassification { get; set; }
}

public class GLClassificationResponse
{
    [JsonPropertyName("ClassCd")]
    public string ClassCd { get; set; } = string.Empty;

    [JsonPropertyName("Rate")]
    public decimal Rate { get; set; }

    [JsonPropertyName("CurrentTermAmt")]
    public ResponseAmount? CurrentTermAmt { get; set; }
}

public class PropertyResponseInfo
{
    [JsonPropertyName("CommlPropertyInfo")]
    public List<PropertyInfoResponse>? CommlPropertyInfo { get; set; }
}

public class PropertyInfoResponse
{
    [JsonPropertyName("Coverage")]
    public List<PropertyCoverageResponse>? Coverage { get; set; }

    [JsonPropertyName("Rate")]
    public decimal Rate { get; set; }

    [JsonPropertyName("CurrentTermAmt")]
    public ResponseAmount? CurrentTermAmt { get; set; }
}

public class PropertyCoverageResponse
{
    [JsonPropertyName("CoverageCd")]
    public string CoverageCd { get; set; } = string.Empty;

    [JsonPropertyName("Rate")]
    public decimal Rate { get; set; }

    [JsonPropertyName("CurrentTermAmt")]
    public ResponseAmount? CurrentTermAmt { get; set; }
}

public class ResponseAmount
{
    [JsonPropertyName("Amt")]
    public decimal Amt { get; set; }
}

public class PolicySummaryInfo
{
    [JsonPropertyName("FullTermAmt")]
    public ResponseAmount? FullTermAmt { get; set; }

    [JsonPropertyName("TotalTaxes")]
    public ResponseAmount? TotalTaxes { get; set; }

    [JsonPropertyName("TotalFees")]
    public ResponseAmount? TotalFees { get; set; }

    [JsonPropertyName("TotalPremium")]
    public ResponseAmount? TotalPremium { get; set; }
}

/// <summary>
/// Carrier-specific response
/// </summary>
public class CarrierResponse
{
    [JsonPropertyName("CarrierName")]
    public string CarrierName { get; set; } = string.Empty;

    [JsonPropertyName("Status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("QuoteNumber")]
    public string QuoteNumber { get; set; } = string.Empty;

    [JsonPropertyName("TotalPremium")]
    public decimal TotalPremium { get; set; }

    [JsonPropertyName("Message")]
    public string? Message { get; set; }

    [JsonPropertyName("Errors")]
    public List<CarrierError>? Errors { get; set; }
}

public class CarrierError
{
    [JsonPropertyName("Code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("Message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("Severity")]
    public string Severity { get; set; } = string.Empty;
}

public class ResponseStatus
{
    [JsonPropertyName("StatusCd")]
    public string StatusCd { get; set; } = string.Empty;

    [JsonPropertyName("StatusDesc")]
    public string StatusDesc { get; set; } = string.Empty;

    [JsonPropertyName("Errors")]
    public List<ResponseError>? Errors { get; set; }
}

public class ResponseError
{
    [JsonPropertyName("ErrorCd")]
    public string ErrorCd { get; set; } = string.Empty;

    [JsonPropertyName("ErrorDesc")]
    public string ErrorDesc { get; set; } = string.Empty;
}
