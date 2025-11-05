using InsuranceWrapperApi.Domain.Enums;

namespace InsuranceWrapperApi.Application.DTOs.Common;

/// <summary>
/// Generic bind request - typically follows a quote
/// </summary>
public class GenericBindRequest
{
    public string QuoteId { get; set; } = string.Empty;
    public LineOfBusiness LineOfBusiness { get; set; }
    public ProviderType Provider { get; set; }
    
    // Payment Information
    public PaymentInfo? Payment { get; set; }
    
    // Additional bind-specific data
    public Dictionary<string, object>? AdditionalBindData { get; set; }
}

public class PaymentInfo
{
    public string PaymentMethod { get; set; } = string.Empty; // CreditCard, ACH, etc.
    public decimal Amount { get; set; }
    public string? CardNumber { get; set; }
    public string? ExpiryDate { get; set; }
    public string? CVV { get; set; }
    public string? AccountNumber { get; set; }
    public string? RoutingNumber { get; set; }
}
