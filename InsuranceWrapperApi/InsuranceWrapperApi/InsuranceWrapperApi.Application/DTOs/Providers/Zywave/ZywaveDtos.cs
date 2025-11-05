namespace InsuranceWrapperApi.Application.DTOs.Providers.Zywave;

/// <summary>
/// Zywave API request structure
/// Note: This is a simplified example. Actual Zywave API structure should be referenced
/// </summary>
public class ZywaveQuoteRequest
{
    public string TransactionId { get; set; } = Guid.NewGuid().ToString();
    public string ActionType { get; set; } = "QUOTE";
    public ZywaveEntity Entity { get; set; } = new();
    public ZywavePolicyInfo PolicyInfo { get; set; } = new();
    public List<ZywaveExposure> Exposures { get; set; } = new();
}

public class ZywaveEntity
{
    public string EntityName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public ZywaveAddress MailingAddress { get; set; } = new();
}

public class ZywaveAddress
{
    public string Line1 { get; set; } = string.Empty;
    public string Line2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string StateProvince { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = "US";
}

public class ZywavePolicyInfo
{
    public string PolicyType { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}

public class ZywaveExposure
{
    public string ExposureType { get; set; } = string.Empty;
    public Dictionary<string, object> ExposureData { get; set; } = new();
    public List<ZywaveCoverageOption> RequestedCoverages { get; set; } = new();
}

public class ZywaveCoverageOption
{
    public string CoverageCode { get; set; } = string.Empty;
    public decimal? RequestedLimit { get; set; }
    public decimal? RequestedDeductible { get; set; }
}

/// <summary>
/// Zywave API response structure
/// </summary>
public class ZywaveQuoteResponse
{
    public string ResponseStatus { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public string QuoteId { get; set; } = string.Empty;
    public ZywavePremiumBreakdown PremiumBreakdown { get; set; } = new();
    public List<ZywaveCoverage>? Coverages { get; set; }
    public DateTime? ExpirationTimestamp { get; set; }
    public List<ZywaveMessage>? Messages { get; set; }
}

public class ZywavePremiumBreakdown
{
    public decimal PolicyPremium { get; set; }
    public decimal SurchargesAndFees { get; set; }
    public decimal TaxesAndAssessments { get; set; }
    public decimal TotalAmount { get; set; }
}

public class ZywaveCoverage
{
    public string Description { get; set; } = string.Empty;
    public decimal? LimitAmount { get; set; }
    public decimal? DeductibleAmount { get; set; }
    public decimal PremiumAmount { get; set; }
}

public class ZywaveMessage
{
    public string Severity { get; set; } = string.Empty;
    public string MessageCode { get; set; } = string.Empty;
    public string MessageText { get; set; } = string.Empty;
}

public class ZywaveBindRequest
{
    public string TransactionId { get; set; } = Guid.NewGuid().ToString();
    public string ActionType { get; set; } = "BIND";
    public string QuoteId { get; set; } = string.Empty;
    public ZywavePaymentInfo PaymentInfo { get; set; } = new();
}

public class ZywavePaymentInfo
{
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal PaymentAmount { get; set; }
    public Dictionary<string, string>? PaymentData { get; set; }
}

public class ZywaveBindResponse
{
    public string ResponseStatus { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public DateTime PolicyEffectiveDate { get; set; }
    public DateTime PolicyExpirationDate { get; set; }
    public decimal TotalPremium { get; set; }
    public List<ZywaveMessage>? Messages { get; set; }
}
