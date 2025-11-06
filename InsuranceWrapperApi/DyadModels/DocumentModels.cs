using System.Text.Json.Serialization;

namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad;

/// <summary>
/// ACE Hub Get Document Request
/// </summary>
public class AceHubDocumentRequest
{
    [JsonPropertyName("SignonRq")]
    public SignonRq SignonRq { get; set; } = new();

    [JsonPropertyName("InsuranceSvcRq")]
    public DocumentInsuranceSvcRq InsuranceSvcRq { get; set; } = new();
}

public class DocumentInsuranceSvcRq
{
    [JsonPropertyName("RqUID")]
    public string RqUID { get; set; } = Guid.NewGuid().ToString();

    [JsonPropertyName("IRH_QuoteNo")]
    public string IRH_QuoteNo { get; set; } = string.Empty;

    [JsonPropertyName("comIRH_CarrierRequestExt")]
    public ComIRH_CarrierRequestExt ComIRH_CarrierRequestExt { get; set; } = new();

    [JsonPropertyName("comIRH_AdditionalQueTypeExt")]
    public string ComIRH_AdditionalQueTypeExt { get; set; } = "JSON";

    [JsonPropertyName("DocumentPolicyQuoteInqRq")]
    public DocumentPolicyQuoteInqRq DocumentPolicyQuoteInqRq { get; set; } = new();
}

public class DocumentPolicyQuoteInqRq
{
    /// <summary>
    /// CPKGE (Commercial Package)
    /// </summary>
    [JsonPropertyName("LOBCd")]
    public string LOBCd { get; set; } = "CPKGE";

    [JsonPropertyName("TransactionRequestDt")]
    public DateTime TransactionRequestDt { get; set; } = DateTime.Now;

    [JsonPropertyName("Producer")]
    public List<DocumentProducer> Producer { get; set; } = new();

    [JsonPropertyName("DocumentList")]
    public DocumentList DocumentList { get; set; } = new();
}

public class DocumentProducer
{
    [JsonPropertyName("GeneralPartyInfo")]
    public DocumentGeneralPartyInfo GeneralPartyInfo { get; set; } = new();

    [JsonPropertyName("ProducerInfo")]
    public ProducerInfo ProducerInfo { get; set; } = new();
}

public class DocumentGeneralPartyInfo
{
    [JsonPropertyName("NameInfo")]
    public DocumentNameInfo NameInfo { get; set; } = new();

    [JsonPropertyName("Communications")]
    public DocumentCommunications Communications { get; set; } = new();

    [JsonPropertyName("Addr")]
    public object Addr { get; set; } = new();
}

public class DocumentNameInfo
{
    [JsonPropertyName("CommlName")]
    public CommlName CommlName { get; set; } = new();

    [JsonPropertyName("PersonName")]
    public object PersonName { get; set; } = new();

    [JsonPropertyName("SupplementaryNameInfo")]
    public object SupplementaryNameInfo { get; set; } = new();
}

public class DocumentCommunications
{
    [JsonPropertyName("EmailInfo")]
    public EmailInfo EmailInfo { get; set; } = new();

    [JsonPropertyName("WebsiteInfo")]
    public object WebsiteInfo { get; set; } = new();
}

public class DocumentList
{
    [JsonPropertyName("DocumentDetail")]
    public List<DocumentDetail> DocumentDetail { get; set; } = new();
}

public class DocumentDetail
{
    /// <summary>
    /// QUOTE, POLICY, APPLICATION, BINDER
    /// </summary>
    [JsonPropertyName("DocumentCd")]
    public string DocumentCd { get; set; } = "QUOTE";

    /// <summary>
    /// AGENT, INSURED, COMPANY
    /// </summary>
    [JsonPropertyName("DocumentCopyType")]
    public string DocumentCopyType { get; set; } = "AGENT";
}

/// <summary>
/// ACE Hub Document Response
/// </summary>
public class AceHubDocumentResponse
{
    [JsonPropertyName("InsuranceSvcRs")]
    public DocumentInsuranceSvcRs? InsuranceSvcRs { get; set; }

    [JsonPropertyName("Status")]
    public ResponseStatus? Status { get; set; }
}

public class DocumentInsuranceSvcRs
{
    [JsonPropertyName("RqUID")]
    public string RqUID { get; set; } = string.Empty;

    [JsonPropertyName("IRH_QuoteNo")]
    public string IRH_QuoteNo { get; set; } = string.Empty;

    [JsonPropertyName("Documents")]
    public List<Document>? Documents { get; set; }
}

public class Document
{
    [JsonPropertyName("DocumentType")]
    public string DocumentType { get; set; } = string.Empty;

    [JsonPropertyName("DocumentFormat")]
    public string DocumentFormat { get; set; } = string.Empty;

    [JsonPropertyName("DocumentBase64")]
    public string DocumentBase64 { get; set; } = string.Empty;

    [JsonPropertyName("FileName")]
    public string FileName { get; set; } = string.Empty;

    [JsonPropertyName("CarrierName")]
    public string CarrierName { get; set; } = string.Empty;
}
