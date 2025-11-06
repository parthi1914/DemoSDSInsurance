using System.Text.Json.Serialization;

namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad;

/// <summary>
/// Commercial Package Policy Quote Inquiry Request
/// </summary>
public class CommlPkgPolicyQuoteInqRq
{
    [JsonPropertyName("FormsRequestedCd")]
    public string FormsRequestedCd { get; set; } = "BOTH";

    [JsonPropertyName("SaveIndicationCd")]
    public string SaveIndicationCd { get; set; } = "Yes";

    [JsonPropertyName("Producer")]
    public List<Producer> Producer { get; set; } = new();

    [JsonPropertyName("InsuredOrPrincipal")]
    public InsuredOrPrincipal InsuredOrPrincipal { get; set; } = new();

    [JsonPropertyName("Policy")]
    public Policy Policy { get; set; } = new();

    [JsonPropertyName("BusinessPurposeTypeCd")]
    public string BusinessPurposeTypeCd { get; set; } = "NBS"; // New Business

    [JsonPropertyName("TransactionRequestDt")]
    public DateTime TransactionRequestDt { get; set; } = DateTime.Now;

    [JsonPropertyName("comIRH_GoverningStateExt")]
    public string ComIRH_GoverningStateExt { get; set; } = string.Empty;

    [JsonPropertyName("Location")]
    public List<Location> Location { get; set; } = new();

    [JsonPropertyName("LocationUWInfo")]
    public List<LocationUWInfo> LocationUWInfo { get; set; } = new();

    [JsonPropertyName("GeneralLiabilityLineBusiness")]
    public GeneralLiabilityLineBusiness? GeneralLiabilityLineBusiness { get; set; }

    [JsonPropertyName("CommlPropertyLineBusiness")]
    public CommlPropertyLineBusiness? CommlPropertyLineBusiness { get; set; }

    [JsonPropertyName("PriorLoss")]
    public PriorLoss PriorLoss { get; set; } = new();
}

/// <summary>
/// Producer (Agent/Broker) Information
/// </summary>
public class Producer
{
    [JsonPropertyName("GeneralPartyInfo")]
    public GeneralPartyInfo GeneralPartyInfo { get; set; } = new();

    [JsonPropertyName("ProducerInfo")]
    public ProducerInfo ProducerInfo { get; set; } = new();
}

public class ProducerInfo
{
    /// <summary>
    /// LoggedInUser or UN (Underwriter)
    /// </summary>
    [JsonPropertyName("ProducerRoleCd")]
    public string ProducerRoleCd { get; set; } = "LoggedInUser";

    [JsonPropertyName("ContractNumber")]
    public string ContractNumber { get; set; } = string.Empty;
}

/// <summary>
/// Insured or Principal Information
/// </summary>
public class InsuredOrPrincipal
{
    [JsonPropertyName("GeneralPartyInfo")]
    public GeneralPartyInfo GeneralPartyInfo { get; set; } = new();

    [JsonPropertyName("InsuredOrPrincipalInfo")]
    public InsuredOrPrincipalInfo InsuredOrPrincipalInfo { get; set; } = new();
}

public class InsuredOrPrincipalInfo
{
    [JsonPropertyName("InsuredOrPrincipalRoleCd")]
    public string InsuredOrPrincipalRoleCd { get; set; } = "Insured";
}

/// <summary>
/// Policy Information
/// </summary>
public class Policy
{
    [JsonPropertyName("PolicyNumber")]
    public string PolicyNumber { get; set; } = string.Empty;

    /// <summary>
    /// Line of Business Code: CPKGE (Commercial Package)
    /// </summary>
    [JsonPropertyName("LOBCd")]
    public string LOBCd { get; set; } = "CPKGE";

    [JsonPropertyName("NAICCd")]
    public string NAICCd { get; set; } = string.Empty;

    [JsonPropertyName("ContractTerm")]
    public ContractTerm ContractTerm { get; set; } = new();

    [JsonPropertyName("Form")]
    public List<string> Form { get; set; } = new();

    [JsonPropertyName("IRH_LossFreeYears")]
    public int? IRH_LossFreeYears { get; set; }

    [JsonPropertyName("IRH_YrsInBus")]
    public int? IRH_YrsInBus { get; set; }

    [JsonPropertyName("NumYrsInBusiness")]
    public int? NumYrsInBusiness { get; set; }
}

public class ContractTerm
{
    [JsonPropertyName("EffectiveDt")]
    public DateTime EffectiveDt { get; set; }

    [JsonPropertyName("ExpirationDt")]
    public DateTime ExpirationDt { get; set; }

    [JsonPropertyName("OriginalPolicyInceptionDt")]
    public DateTime? OriginalPolicyInceptionDt { get; set; }
}
