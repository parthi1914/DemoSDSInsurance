# ACE Hub (Dyad) API Models for .NET 8

Complete, production-ready C# models for integrating with Dyad's ACE Hub 2.0 API.

## 📦 What's Included

- ✅ **Authentication Models** - OAuth 2.0 token request/response
- ✅ **Rate Request Models** - Complete commercial package quote structure
- ✅ **General Liability Models** - GL coverages and classifications
- ✅ **Property Models** - Building and BPP coverages
- ✅ **Location Models** - Building details and underwriting info
- ✅ **Document Models** - Quote and policy document retrieval
- ✅ **Response Models** - Structured response parsing
- ✅ **Refit Interface** - Type-safe HTTP client
- ✅ **Extension Methods** - Helper methods for common workflows

## 🚀 Quick Start

### Step 1: Install NuGet Packages

```bash
dotnet add package Refit
dotnet add package Refit.HttpClientFactory
dotnet add package Microsoft.Extensions.Http.Polly
```

### Step 2: Register in DI Container

```csharp
// In Program.cs or Startup.cs
services.AddRefitClient<IAceHubApiClient>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://api.acehub.dyad.com"); // Replace with actual base URL
        c.Timeout = TimeSpan.FromSeconds(60);
    })
    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, retryAttempt => 
        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
    .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
```

### Step 3: Use in Your Service

```csharp
public class InsuranceQuoteService
{
    private readonly IAceHubApiClient _aceHubClient;
    private readonly IConfiguration _configuration;

    public InsuranceQuoteService(IAceHubApiClient aceHubClient, IConfiguration configuration)
    {
        _aceHubClient = aceHubClient;
        _configuration = configuration;
    }

    public async Task<AceHubRateResponse> GetQuoteAsync()
    {
        var clientId = _configuration["AceHub:ClientId"];
        var clientSecret = _configuration["AceHub:ClientSecret"];

        var request = BuildRateRequest();
        
        // Use extension method with automatic authentication
        return await _aceHubClient.GetRateWithAuthAsync(
            request, 
            clientId, 
            clientSecret);
    }
}
```

## 📝 Usage Examples

### Example 1: Simple GL Quote

```csharp
public AceHubRateRequest CreateSimpleGLQuote()
{
    return new AceHubRateRequest
    {
        SignonRq = new SignonRq
        {
            SignonPswd = new SignonPswd
            {
                CustId = new CustId { CustLoginId = "YOUR_CLIENT_ID" }
            }
        },
        InsuranceSvcRq = new InsuranceSvcRq
        {
            RqUID = Guid.NewGuid().ToString(),
            IRH_QuoteNo = "Q-2025-001",
            IRH_Application_Name = "Your App Name",
            IRH_Application_Type = "QRB",
            IRH_Request_Type = "RATE",
            
            // Specify carriers to quote
            ComIRH_CarrierRequestExt = new ComIRH_CarrierRequestExt
            {
                ComIRH_CarrierInfoExt = new List<ComIRH_CarrierInfoExt>
                {
                    new() { IRH_CarrierName = "ACI", IRH_AdmittedStatus = "N" },
                    new() { IRH_CarrierName = "AMTrust", IRH_AdmittedStatus = "N" }
                }
            },
            
            // Main quote request
            CommlPkgPolicyQuoteInqRq = new CommlPkgPolicyQuoteInqRq
            {
                // Producer info
                Producer = new List<Producer>
                {
                    new()
                    {
                        GeneralPartyInfo = new GeneralPartyInfo
                        {
                            NameInfo = new NameInfo
                            {
                                CommlName = new CommlName { CommercialName = "Agent Name" }
                            },
                            Communications = new Communications
                            {
                                EmailInfo = new EmailInfo { EmailAddr = "agent@agency.com" },
                                PhoneInfo = new PhoneInfo { PhoneNumber = "555-1234" }
                            }
                        },
                        ProducerInfo = new ProducerInfo { ProducerRoleCd = "LoggedInUser" }
                    }
                },
                
                // Insured info
                InsuredOrPrincipal = new InsuredOrPrincipal
                {
                    GeneralPartyInfo = new GeneralPartyInfo
                    {
                        NameInfo = new NameInfo
                        {
                            CommlName = new CommlName { CommercialName = "ABC Construction" },
                            PersonName = new PersonName
                            {
                                GivenName = "John",
                                Surname = "Doe"
                            }
                        },
                        Communications = new Communications
                        {
                            EmailInfo = new EmailInfo { EmailAddr = "john@abc.com" },
                            PhoneInfo = new PhoneInfo { PhoneNumber = "555-5678" }
                        },
                        Addr = new Addr
                        {
                            Addr1 = "123 Main St",
                            City = "Indianapolis",
                            StateProvCd = "IN",
                            PostalCode = "46240",
                            County = "Marion"
                        }
                    }
                },
                
                // Policy info
                Policy = new Policy
                {
                    LOBCd = "CPKGE",
                    ContractTerm = new ContractTerm
                    {
                        EffectiveDt = DateTime.Now.AddDays(30),
                        ExpirationDt = DateTime.Now.AddYears(1).AddDays(30)
                    }
                },
                
                BusinessPurposeTypeCd = "NBS",
                TransactionRequestDt = DateTime.Now,
                ComIRH_GoverningStateExt = "IN",
                
                // Location
                Location = new List<Location>
                {
                    new()
                    {
                        Id = 1,
                        Addr = new Addr
                        {
                            Addr1 = "123 Main St",
                            City = "Indianapolis",
                            StateProvCd = "IN",
                            PostalCode = "46240",
                            County = "Marion",
                            TerritoryCd = "503"
                        }
                    }
                },
                
                // GL Coverage
                GeneralLiabilityLineBusiness = new GeneralLiabilityLineBusiness
                {
                    LiabilityInfo = new LiabilityInfo
                    {
                        // Standard GL coverages
                        Coverage = new List<GLCoverage>
                        {
                            new()
                            {
                                CoverageCd = "GENAG",
                                CoverageDesc = "General Aggregate Limit",
                                Limit = new List<Limit>
                                {
                                    new()
                                    {
                                        FormatCurrencyAmt = new FormatCurrencyAmt { Amt = "2000000" },
                                        LimitAppliesToCd = "Aggregate"
                                    }
                                }
                            },
                            new()
                            {
                                CoverageCd = "EAOCC",
                                CoverageDesc = "Each Occurrence Limit",
                                Limit = new List<Limit>
                                {
                                    new()
                                    {
                                        FormatCurrencyAmt = new FormatCurrencyAmt { Amt = "1000000" },
                                        LimitAppliesToCd = "PerOcc"
                                    }
                                }
                            }
                        },
                        
                        // GL Classifications (class codes)
                        GeneralLiabilityClassification = new List<GeneralLiabilityClassification>
                        {
                            new()
                            {
                                ClassCd = "91342",
                                ClassCdDesc = "Carpentry - NOC",
                                Exposure = 50000, // $50,000 payroll
                                PremiumBasisCd = "PAYRL",
                                LocationRef = "1",
                                Coverage = new List<ClassificationCoverage>
                                {
                                    new() { CoverageCd = "PREM" },
                                    new() { CoverageCd = "PRDCO" }
                                }
                            }
                        }
                    }
                }
            }
        }
    };
}
```

### Example 2: Property Quote

```csharp
public void AddPropertyCoverage(CommlPkgPolicyQuoteInqRq request)
{
    // Add location underwriting info
    request.LocationUWInfo = new List<LocationUWInfo>
    {
        new()
        {
            LocationRef = 1,
            SubLocationRef = 1,
            TerritoryCd = "503",
            
            Construction = new Construction
            {
                ConstructionCd = "JM", // Joisted Masonry
                YearBuilt = "1995",
                BldgArea = new BldgArea { NumUnits = 5000 },
                NumStories = "2"
            },
            
            BldgProtection = new BldgProtection
            {
                ProtectionClassGradeCd = "5",
                ProtectionDeviceSprinklerCd = "YES"
            },
            
            BldgImprovements = new BldgImprovements
            {
                RoofingImprovementYear = "2020",
                HeatingImprovementYear = "2018",
                WiringImprovementYear = "1995",
                PlumbingImprovementYear = "1995"
            }
        }
    };
    
    // Add property coverage
    request.CommlPropertyLineBusiness = new CommlPropertyLineBusiness
    {
        PropertyInfo = new PropertyInfo
        {
            CommlPropertyInfo = new List<CommlPropertyInfo>
            {
                new()
                {
                    ClassCd = "0311",
                    ClassCdDesc = "Apartments",
                    LocationRef = 1,
                    SubLocationRef = 1,
                    
                    Coverage = new List<PropertyCoverage>
                    {
                        // Building coverage
                        new()
                        {
                            CoverageCd = "BLDG",
                            CoverageDesc = "Building",
                            Limit = new List<PropertyLimit>
                            {
                                new()
                                {
                                    FormatCurrencyAmt = new FormatCurrencyAmt { Amt = "500000" },
                                    ValuationCd = "RC" // Replacement Cost
                                }
                            },
                            Deductible = new List<Deductible>
                            {
                                new()
                                {
                                    FormatInteger = 2500,
                                    DeductibleAppliesToCd = "AllPeril"
                                }
                            },
                            CommlCoverageSupplement = new CommlCoverageSupplement
                            {
                                CoverageSubCd = "SPECIAL", // Special form
                                CoinsurancePct = "90"
                            }
                        },
                        
                        // Business Personal Property
                        new()
                        {
                            CoverageCd = "BPP",
                            CoverageDesc = "Business Personal Property",
                            Limit = new List<PropertyLimit>
                            {
                                new()
                                {
                                    FormatCurrencyAmt = new FormatCurrencyAmt { Amt = "100000" },
                                    ValuationCd = "ACV" // Actual Cash Value
                                }
                            },
                            Deductible = new List<Deductible>
                            {
                                new()
                                {
                                    FormatInteger = 2500,
                                    DeductibleAppliesToCd = "AllPeril"
                                }
                            },
                            CommlCoverageSupplement = new CommlCoverageSupplement
                            {
                                CoverageSubCd = "BASIC",
                                CoinsurancePct = "80"
                            }
                        }
                    }
                }
            }
        }
    };
}
```

### Example 3: Get Quote Documents

```csharp
public async Task<byte[]> GetQuoteDocumentAsync(string quoteNumber, string carrierName)
{
    var clientId = _configuration["AceHub:ClientId"];
    var clientSecret = _configuration["AceHub:ClientSecret"];
    
    var request = new AceHubDocumentRequest
    {
        SignonRq = new SignonRq
        {
            SignonPswd = new SignonPswd
            {
                CustId = new CustId { CustLoginId = clientId }
            }
        },
        InsuranceSvcRq = new DocumentInsuranceSvcRq
        {
            IRH_QuoteNo = quoteNumber,
            ComIRH_CarrierRequestExt = new ComIRH_CarrierRequestExt
            {
                ComIRH_CarrierInfoExt = new List<ComIRH_CarrierInfoExt>
                {
                    new() { IRH_CarrierName = carrierName }
                }
            },
            DocumentPolicyQuoteInqRq = new DocumentPolicyQuoteInqRq
            {
                LOBCd = "CPKGE",
                Producer = new List<DocumentProducer>
                {
                    new()
                    {
                        GeneralPartyInfo = new DocumentGeneralPartyInfo
                        {
                            NameInfo = new DocumentNameInfo
                            {
                                CommlName = new CommlName { CommercialName = "Agent Name" }
                            },
                            Communications = new DocumentCommunications
                            {
                                EmailInfo = new EmailInfo { EmailAddr = "agent@agency.com" }
                            }
                        },
                        ProducerInfo = new ProducerInfo { ProducerRoleCd = "LoggedInUser" }
                    }
                },
                DocumentList = new DocumentList
                {
                    DocumentDetail = new List<DocumentDetail>
                    {
                        new()
                        {
                            DocumentCd = "QUOTE",
                            DocumentCopyType = "AGENT"
                        }
                    }
                }
            }
        }
    };
    
    var response = await _aceHubClient.GetDocumentWithAuthAsync(
        request, 
        clientId, 
        clientSecret);
    
    if (response?.InsuranceSvcRs?.Documents?.Any() == true)
    {
        var document = response.InsuranceSvcRs.Documents.First();
        return Convert.FromBase64String(document.DocumentBase64);
    }
    
    return Array.Empty<byte>();
}
```

### Example 4: Process Response

```csharp
public void ProcessRateResponse(AceHubRateResponse response)
{
    if (response?.InsuranceSvcRs == null)
    {
        Console.WriteLine("No response received");
        return;
    }
    
    Console.WriteLine($"Quote Number: {response.InsuranceSvcRs.IRH_QuoteNo}");
    
    // Check each carrier response
    if (response.InsuranceSvcRs.Carriers != null)
    {
        foreach (var carrier in response.InsuranceSvcRs.Carriers)
        {
            Console.WriteLine($"\nCarrier: {carrier.CarrierName}");
            Console.WriteLine($"Status: {carrier.Status}");
            Console.WriteLine($"Quote Number: {carrier.QuoteNumber}");
            Console.WriteLine($"Premium: ${carrier.TotalPremium:N2}");
            
            if (carrier.Errors?.Any() == true)
            {
                Console.WriteLine("Errors:");
                foreach (var error in carrier.Errors)
                {
                    Console.WriteLine($"  - {error.Message}");
                }
            }
        }
    }
    
    // Get policy summary
    var summary = response.InsuranceSvcRs.CommlPkgPolicyQuoteInqRs?.PolicySummaryInfo;
    if (summary != null)
    {
        Console.WriteLine($"\nTotal Premium: ${summary.TotalPremium?.Amt:N2}");
        Console.WriteLine($"Taxes: ${summary.TotalTaxes?.Amt:N2}");
        Console.WriteLine($"Fees: ${summary.TotalFees?.Amt:N2}");
    }
}
```

## 🔑 Configuration

### appsettings.json

```json
{
  "AceHub": {
    "BaseUrl": "https://api.acehub.dyad.com",
    "ClientId": "YOUR_CLIENT_ID",
    "ClientSecret": "YOUR_CLIENT_SECRET",
    "Timeout": 60
  }
}
```

## 📋 Supported Carriers

The ACE Hub supports multiple carriers:
- **ACI** - American Contractors Indemnity
- **AMTrust** - AM Trust Financial
- **Hartford** - The Hartford
- **Travelers** - Travelers Insurance
- **Others** - Check with Dyad for full list

## 🎯 Common Class Codes

### General Liability
- `60010` - Apartment Buildings
- `91342` - Carpentry - NOC
- `91343` - Contractors - General NOC
- `91583` - Plumbing NOC
- `13150` - Contractors - Commercial

### Property
- `0311` - Apartments (up to 10 units)
- `0312` - Apartments (over 10 units)
- `0612` - Office Buildings
- `0613` - Retail Stores

## 🏗️ LOB Codes

- `CPKGE` - Commercial Package Policy
- `GL` - General Liability Only
- `CP` - Commercial Property Only
- `WC` - Workers Compensation

## ⚠️ Important Notes

1. **Authentication**: Always get a fresh token before making rate or document calls
2. **Timeout**: ACE Hub API can take 30-60 seconds for multi-carrier quotes
3. **Carrier Selection**: Specify carriers in `ComIRH_CarrierInfoExt` array
4. **Required Fields**: Business name, address, effective date, and at least one coverage
5. **Class Codes**: Use proper ISO class codes for accurate rating
6. **Territory Codes**: Required for proper rating (varies by state)

## 🔒 Security Best Practices

1. Store credentials in Azure Key Vault or AWS Secrets Manager
2. Use environment variables for sensitive data
3. Implement token caching (tokens valid for specific duration)
4. Log API calls but never log credentials
5. Use HTTPS only

## 📚 Additional Resources

- ACE Hub API Documentation
- Dyad Support Portal
- ISO Class Code Reference
- NCCI Class Code Guide

## 🆘 Troubleshooting

### Issue: "Invalid Client Credentials"
**Solution**: Verify ClientId and ClientSecret in configuration

### Issue: "Timeout Error"
**Solution**: Increase timeout to 60+ seconds for multi-carrier quotes

### Issue: "Invalid Class Code"
**Solution**: Use valid ISO class codes for the specific LOB

### Issue: "Territory Code Required"
**Solution**: Ensure territory code is populated in location address

## 📞 Support

For ACE Hub API support:
- Email: support@dyad.com
- Documentation: https://docs.acehub.dyad.com

---

**Note**: These models are based on ACE Hub 2.0 API. Always refer to the latest API documentation for any updates or changes.
