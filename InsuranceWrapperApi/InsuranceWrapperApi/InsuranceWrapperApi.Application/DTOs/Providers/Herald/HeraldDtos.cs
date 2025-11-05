namespace InsuranceWrapperApi.Application.DTOs.Providers.Herald;

/// <summary>
/// Herald API request structure
/// Note: This is a simplified example. Actual Herald API structure should be referenced
/// </summary>
public class HeraldQuoteRequest
{
    public string RequestId { get; set; } = Guid.NewGuid().ToString();
    public string Operation { get; set; } = "GetQuote";
    public HeraldApplicant Applicant { get; set; } = new();
    public HeraldCoverage Coverage { get; set; } = new();
    public HeraldRiskData RiskData { get; set; } = new();
}

public class HeraldApplicant
{
    public string BusinessName { get; set; } = string.Empty;
    public string PrimaryContact { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public HeraldLocation Location { get; set; } = new();
}

public class HeraldLocation
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
}

public class HeraldCoverage
{
    public string ProductLine { get; set; } = string.Empty;
    public DateTime PolicyStartDate { get; set; }
    public int PolicyTermMonths { get; set; } = 12;
    public List<HeraldCoverageLimit> Limits { get; set; } = new();
}

public class HeraldCoverageLimit
{
    public string LimitType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal? DeductibleAmount { get; set; }
}

public class HeraldRiskData
{
    public Dictionary<string, object> Attributes { get; set; } = new();
}

/// <summary>
/// Herald API response structure
/// </summary>
public class HeraldQuoteResponse
{
    public string Status { get; set; } = string.Empty;
    public string QuoteReference { get; set; } = string.Empty;
    public HeraldPremiumDetail PremiumDetail { get; set; } = new();
    public List<HeraldCoverageDetail>? CoverageDetails { get; set; }
    public DateTime? ValidUntil { get; set; }
    public List<HeraldError>? Errors { get; set; }
}

public class HeraldPremiumDetail
{
    public decimal Base { get; set; }
    public decimal Tax { get; set; }
    public decimal Fee { get; set; }
    public decimal Total { get; set; }
}

public class HeraldCoverageDetail
{
    public string CoverageType { get; set; } = string.Empty;
    public decimal Limit { get; set; }
    public decimal Deductible { get; set; }
    public decimal Premium { get; set; }
}

public class HeraldError
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class HeraldBindRequest
{
    public string RequestId { get; set; } = Guid.NewGuid().ToString();
    public string QuoteReference { get; set; } = string.Empty;
    public string Operation { get; set; } = "BindPolicy";
    public HeraldPaymentDetails PaymentDetails { get; set; } = new();
}

public class HeraldPaymentDetails
{
    public string Method { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public Dictionary<string, string>? Details { get; set; }
}

public class HeraldBindResponse
{
    public string Status { get; set; } = string.Empty;
    public string PolicyId { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal Premium { get; set; }
    public List<HeraldError>? Errors { get; set; }
}
