using System.Text.Json.Serialization;

namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad;

/// <summary>
/// Main ACE Hub Rate Request
/// </summary>
public class AceHubRateRequest
{
    [JsonPropertyName("SignonRq")]
    public SignonRq SignonRq { get; set; } = new();

    [JsonPropertyName("InsuranceSvcRq")]
    public InsuranceSvcRq InsuranceSvcRq { get; set; } = new();
}

/// <summary>
/// Signon Request for authentication
/// </summary>
public class SignonRq
{
    [JsonPropertyName("SignonPswd")]
    public SignonPswd SignonPswd { get; set; } = new();
}

public class SignonPswd
{
    [JsonPropertyName("CustId")]
    public CustId CustId { get; set; } = new();

    [JsonPropertyName("CustPswd")]
    public CustPswd? CustPswd { get; set; }
}

public class CustId
{
    [JsonPropertyName("CustLoginId")]
    public string CustLoginId { get; set; } = string.Empty;
}

public class CustPswd
{
    [JsonPropertyName("Pswd")]
    public string? Pswd { get; set; }
}

/// <summary>
/// Main Insurance Service Request
/// </summary>
public class InsuranceSvcRq
{
    [JsonPropertyName("RqUID")]
    public string RqUID { get; set; } = Guid.NewGuid().ToString();

    [JsonPropertyName("IRH_QuoteNo")]
    public string IRH_QuoteNo { get; set; } = string.Empty;

    [JsonPropertyName("IRH_Application_Name")]
    public string IRH_Application_Name { get; set; } = string.Empty;

    [JsonPropertyName("IRH_Application_Type")]
    public string IRH_Application_Type { get; set; } = "QRB";

    [JsonPropertyName("IRH_Request_Type")]
    public string IRH_Request_Type { get; set; } = "RATE";

    [JsonPropertyName("comIRH_CarrierRequestExt")]
    public ComIRH_CarrierRequestExt ComIRH_CarrierRequestExt { get; set; } = new();

    [JsonPropertyName("comIRH_AdditionalQueRequireExt")]
    public string? ComIRH_AdditionalQueRequireExt { get; set; }

    [JsonPropertyName("comIRH_AdditionalQueTypeExt")]
    public string? ComIRH_AdditionalQueTypeExt { get; set; }

    [JsonPropertyName("CommlPkgPolicyQuoteInqRq")]
    public CommlPkgPolicyQuoteInqRq CommlPkgPolicyQuoteInqRq { get; set; } = new();
}

/// <summary>
/// Carrier Request Extension - specifies which carriers to quote
/// </summary>
public class ComIRH_CarrierRequestExt
{
    [JsonPropertyName("comIRH_CarrierInfoExt")]
    public List<ComIRH_CarrierInfoExt> ComIRH_CarrierInfoExt { get; set; } = new();
}

public class ComIRH_CarrierInfoExt
{
    /// <summary>
    /// Carrier name: ACI, AMTrust, Hartford, Travelers, etc.
    /// </summary>
    [JsonPropertyName("IRH_CarrierName")]
    public string IRH_CarrierName { get; set; } = string.Empty;

    [JsonPropertyName("IRH_ReRateRqUID")]
    public string IRH_ReRateRqUID { get; set; } = string.Empty;

    [JsonPropertyName("IRH_AdmittedStatus")]
    public string IRH_AdmittedStatus { get; set; } = "N";

    [JsonPropertyName("IRH_TimeTaken")]
    public int? IRH_TimeTaken { get; set; }

    [JsonPropertyName("IRH_OfficeCd")]
    public string IRH_OfficeCd { get; set; } = string.Empty;
}
