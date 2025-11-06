using System.Text.Json.Serialization;

namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad;

/// <summary>
/// General Party Information (used for Producer, Insured, etc.)
/// </summary>
public class GeneralPartyInfo
{
    [JsonPropertyName("NameInfo")]
    public NameInfo NameInfo { get; set; } = new();

    [JsonPropertyName("Communications")]
    public Communications Communications { get; set; } = new();

    [JsonPropertyName("Addr")]
    public Addr Addr { get; set; } = new();

    [JsonPropertyName("PhysicalAddr")]
    public Addr? PhysicalAddr { get; set; }

    [JsonPropertyName("PersonInfo")]
    public PersonInfo PersonInfo { get; set; } = new();

    [JsonPropertyName("OperationsDesc")]
    public string OperationsDesc { get; set; } = string.Empty;
}

/// <summary>
/// Name Information
/// </summary>
public class NameInfo
{
    [JsonPropertyName("CommlName")]
    public CommlName CommlName { get; set; } = new();

    [JsonPropertyName("PersonName")]
    public PersonName PersonName { get; set; } = new();

    [JsonPropertyName("SupplementaryNameInfo")]
    public SupplementaryNameInfo SupplementaryNameInfo { get; set; } = new();

    [JsonPropertyName("IRH_ReferenceInsuredID")]
    public string IRH_ReferenceInsuredID { get; set; } = string.Empty;

    [JsonPropertyName("LegalEntityCd")]
    public string? LegalEntityCd { get; set; }
}

public class CommlName
{
    [JsonPropertyName("CommercialName")]
    public string CommercialName { get; set; } = string.Empty;
}

public class PersonName
{
    [JsonPropertyName("Surname")]
    public string Surname { get; set; } = string.Empty;

    [JsonPropertyName("GivenName")]
    public string GivenName { get; set; } = string.Empty;

    [JsonPropertyName("MiddleName")]
    public string MiddleName { get; set; } = string.Empty;
}

public class SupplementaryNameInfo
{
    [JsonPropertyName("SupplementaryName")]
    public string SupplementaryName { get; set; } = string.Empty;

    [JsonPropertyName("SupplementaryNameCd")]
    public string SupplementaryNameCd { get; set; } = string.Empty;
}

/// <summary>
/// Communications (Email, Phone, Website)
/// </summary>
public class Communications
{
    [JsonPropertyName("EmailInfo")]
    public EmailInfo EmailInfo { get; set; } = new();

    [JsonPropertyName("WebsiteInfo")]
    public WebsiteInfo WebsiteInfo { get; set; } = new();

    [JsonPropertyName("PhoneInfo")]
    public PhoneInfo PhoneInfo { get; set; } = new();
}

public class EmailInfo
{
    [JsonPropertyName("CommunicationUseCd")]
    public string CommunicationUseCd { get; set; } = string.Empty;

    [JsonPropertyName("EmailAddr")]
    public string EmailAddr { get; set; } = string.Empty;
}

public class WebsiteInfo
{
    [JsonPropertyName("WebsiteURL")]
    public string WebsiteURL { get; set; } = string.Empty;

    [JsonPropertyName("IRH_Insured_WebsiteURL")]
    public string? IRH_Insured_WebsiteURL { get; set; }
}

public class PhoneInfo
{
    [JsonPropertyName("PhoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("Fax")]
    public string Fax { get; set; } = string.Empty;
}

/// <summary>
/// Address Information
/// </summary>
public class Addr
{
    [JsonPropertyName("Addr1")]
    public string Addr1 { get; set; } = string.Empty;

    [JsonPropertyName("Addr2")]
    public string Addr2 { get; set; } = string.Empty;

    [JsonPropertyName("City")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("StateProvCd")]
    public string StateProvCd { get; set; } = string.Empty;

    [JsonPropertyName("PostalCode")]
    public string PostalCode { get; set; } = string.Empty;

    [JsonPropertyName("Latitude")]
    public decimal? Latitude { get; set; }

    [JsonPropertyName("Longitude")]
    public decimal? Longitude { get; set; }

    [JsonPropertyName("County")]
    public string County { get; set; } = string.Empty;

    [JsonPropertyName("TerritoryCd")]
    public string TerritoryCd { get; set; } = string.Empty;

    [JsonPropertyName("HouseNumber")]
    public string? HouseNumber { get; set; }

    [JsonPropertyName("StreetNumber")]
    public string StreetNumber { get; set; } = string.Empty;
}

/// <summary>
/// Person Information
/// </summary>
public class PersonInfo
{
    [JsonPropertyName("BirthDt")]
    public DateTime? BirthDt { get; set; }

    [JsonPropertyName("OccupationClassCd")]
    public string OccupationClassCd { get; set; } = string.Empty;

    [JsonPropertyName("TaxIdentity")]
    public List<string> TaxIdentity { get; set; } = new();
}
