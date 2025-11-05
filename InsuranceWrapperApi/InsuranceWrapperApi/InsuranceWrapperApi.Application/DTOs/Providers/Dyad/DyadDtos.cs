namespace InsuranceWrapperApi.Application.DTOs.Providers.Dyad;

/// <summary>
/// Dyad API request structure
/// Note: This is a simplified example. Actual Dyad API structure should be referenced
/// </summary>
public class DyadQuoteRequest
{
    public string TransactionType { get; set; } = "Quote";
    public DyadInsured Insured { get; set; } = new();
    public DyadPolicy Policy { get; set; } = new();
    public DyadCoverages Coverages { get; set; } = new();
    public Dictionary<string, object>? AdditionalData { get; set; }
}

public class DyadInsured
{
    public string Name { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DyadAddress Address { get; set; } = new();
    public string TaxId { get; set; } = string.Empty;
}

public class DyadAddress
{
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string StateCode { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string CountyName { get; set; } = string.Empty;
}

public class DyadPolicy
{
    public string LineOfBusiness { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string PolicyTerm { get; set; } = "12"; // months
}

public class DyadCoverages
{
    public List<DyadCoverage> CoverageList { get; set; } = new();
}

public class DyadCoverage
{
    public string CoverageCode { get; set; } = string.Empty;
    public decimal? Limit { get; set; }
    public decimal? Deductible { get; set; }
    public Dictionary<string, object>? CoverageDetails { get; set; }
}

/// <summary>
/// Dyad API response structure
/// </summary>
public class DyadQuoteResponse
{
    public bool IsSuccessful { get; set; }
    public string QuoteNumber { get; set; } = string.Empty;
    public DyadPremium Premium { get; set; } = new();
    public List<DyadCoverageResponse>? Coverages { get; set; }
    public DateTime? QuoteExpiration { get; set; }
    public List<string>? ErrorMessages { get; set; }
    public Dictionary<string, object>? ResponseData { get; set; }
}

public class DyadPremium
{
    public decimal BasePremium { get; set; }
    public decimal Taxes { get; set; }
    public decimal Fees { get; set; }
    public decimal TotalPremium { get; set; }
}

public class DyadCoverageResponse
{
    public string CoverageName { get; set; } = string.Empty;
    public decimal? Limit { get; set; }
    public decimal? Deductible { get; set; }
    public decimal Premium { get; set; }
}

public class DyadBindRequest
{
    public string QuoteNumber { get; set; } = string.Empty;
    public DyadPayment Payment { get; set; } = new();
}

public class DyadPayment
{
    public string PaymentType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public Dictionary<string, string>? PaymentDetails { get; set; }
}

public class DyadBindResponse
{
    public bool IsSuccessful { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal BoundPremium { get; set; }
    public List<string>? ErrorMessages { get; set; }
}
