# ACE Hub Quick Reference

## 🚀 Common Scenarios

### Scenario 1: Basic GL Quote for Construction Company

```csharp
var request = new AceHubRateRequest
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
        IRH_QuoteNo = "Q-2025-001",
        ComIRH_CarrierRequestExt = new ComIRH_CarrierRequestExt
        {
            ComIRH_CarrierInfoExt = new List<ComIRH_CarrierInfoExt>
            {
                new() { IRH_CarrierName = "Hartford", IRH_AdmittedStatus = "N" }
            }
        },
        CommlPkgPolicyQuoteInqRq = new CommlPkgPolicyQuoteInqRq
        {
            InsuredOrPrincipal = new InsuredOrPrincipal
            {
                GeneralPartyInfo = new GeneralPartyInfo
                {
                    NameInfo = new NameInfo
                    {
                        CommlName = new CommlName { CommercialName = "ABC Construction" }
                    },
                    Addr = new Addr
                    {
                        Addr1 = "123 Main St",
                        City = "Indianapolis",
                        StateProvCd = "IN",
                        PostalCode = "46240"
                    }
                }
            },
            Policy = new Policy
            {
                ContractTerm = new ContractTerm
                {
                    EffectiveDt = DateTime.Now.AddDays(30),
                    ExpirationDt = DateTime.Now.AddYears(1).AddDays(30)
                }
            },
            GeneralLiabilityLineBusiness = new GeneralLiabilityLineBusiness
            {
                LiabilityInfo = new LiabilityInfo
                {
                    GeneralLiabilityClassification = new List<GeneralLiabilityClassification>
                    {
                        new()
                        {
                            ClassCd = "91343", // Contractors - General NOC
                            Exposure = 500000, // $500k payroll
                            PremiumBasisCd = "PAYRL"
                        }
                    }
                }
            }
        }
    }
};
```

### Scenario 2: Multiple Carriers Quote

```csharp
ComIRH_CarrierRequestExt = new ComIRH_CarrierRequestExt
{
    ComIRH_CarrierInfoExt = new List<ComIRH_CarrierInfoExt>
    {
        new() { IRH_CarrierName = "Hartford", IRH_AdmittedStatus = "N" },
        new() { IRH_CarrierName = "Travelers", IRH_AdmittedStatus = "N" },
        new() { IRH_CarrierName = "ACI", IRH_AdmittedStatus = "N" },
        new() { IRH_CarrierName = "AMTrust", IRH_AdmittedStatus = "N" }
    }
}
```

### Scenario 3: GL + Property Package

```csharp
// Add both GL and Property
CommlPkgPolicyQuoteInqRq = new CommlPkgPolicyQuoteInqRq
{
    // ... insured and policy info ...
    
    // GL Coverage
    GeneralLiabilityLineBusiness = new GeneralLiabilityLineBusiness { /* ... */ },
    
    // Property Coverage
    CommlPropertyLineBusiness = new CommlPropertyLineBusiness { /* ... */ }
}
```

## 📊 Coverage Code Reference

### General Liability Coverage Codes
| Code | Description | Typical Limit |
|------|-------------|---------------|
| GENAG | General Aggregate | $2,000,000 |
| EAOCC | Each Occurrence | $1,000,000 |
| PRDCO | Products/Completed Ops | $2,000,000 |
| PIADV | Personal & Advertising Injury | $1,000,000 |
| MEDEX | Medical Expense | $5,000 |
| FIRDM | Damage to Rented Premises | $100,000 |

### Property Coverage Codes
| Code | Description |
|------|-------------|
| BLDG | Building |
| BPP | Business Personal Property |
| BI | Business Income |
| EQKB | Equipment Breakdown |
| SIGN | Signs |
| OUTP | Outdoor Property |

### Coverage Form Codes
| Code | Description |
|------|-------------|
| BASIC | Basic Form (Named Perils) |
| BROAD | Broad Form |
| SPECIAL | Special Form (All Risk) |

## 🏗️ Construction Codes

| Code | Description |
|------|-------------|
| F | Frame |
| JM | Joisted Masonry |
| MNC | Masonry Non-Combustible |
| MNCB | Masonry Non-Combustible with Steel Beams |
| FB | Fire Resistive |

## 🔢 Common Class Codes

### Contractors
| Class Code | Description | Premium Basis |
|------------|-------------|---------------|
| 91343 | Contractors - General NOC | Payroll |
| 91342 | Carpentry - NOC | Payroll |
| 91340 | Contractors - Residential | Payroll |
| 91583 | Plumbing NOC | Payroll |
| 91805 | Roofing - All Types | Payroll/Sales |

### Apartments/Rental
| Class Code | Description | Premium Basis |
|------------|-------------|---------------|
| 60010 | Apartment Buildings | Units |
| 61213 | Condominium Associations | Units |
| 61651 | Lessors Risk Only | Sales |

### Retail
| Class Code | Description | Premium Basis |
|------------|-------------|---------------|
| 10022 | Automobile Service/Storage | Area |
| 12020 | Clothing Store | Area |
| 16642 | Restaurant - Fast Food | Area/Sales |
| 16645 | Restaurant - Table Service | Area/Sales |

## 🎯 Premium Basis Codes

| Code | Description |
|------|-------------|
| PAYRL | Payroll ($1,000) |
| SALES | Sales ($1,000) |
| Unit | Per Unit (apartments, etc.) |
| Area | Square Footage |
| ADMIS | Admissions |
| SQFT | Square Feet |

## 🌐 Territory Codes (Examples)

| State | Territory | Notes |
|-------|-----------|-------|
| IN | 503 | Indianapolis |
| IN | 501 | Northern Indiana |
| IL | 101 | Chicago |
| OH | 305 | Columbus |
| TX | 701 | Houston |

*Territory codes vary by carrier - check with specific carrier*

## ⚡ Quick Tips

### Minimum Required Fields
```csharp
✓ Business Name
✓ Address (complete with state, zip, county)
✓ Effective Date
✓ At least one Class Code with Exposure
✓ Territory Code
✓ Carrier Name
```

### Performance Tips
1. **Parallel Quotes**: Request multiple carriers in one call
2. **Caching**: Cache token for duration (check expiry)
3. **Timeout**: Set to 60+ seconds for multi-carrier
4. **Async**: Always use async/await

### Error Handling
```csharp
try
{
    var response = await client.GetRateWithAuthAsync(request, clientId, clientSecret);
    
    if (response?.InsuranceSvcRs?.Carriers != null)
    {
        foreach (var carrier in response.InsuranceSvcRs.Carriers)
        {
            if (carrier.Status == "ERROR")
            {
                // Handle carrier-specific errors
                LogErrors(carrier.Errors);
            }
            else
            {
                // Process successful quote
                ProcessQuote(carrier);
            }
        }
    }
}
catch (ApiException ex)
{
    // Handle API errors
    _logger.LogError(ex, "ACE Hub API error");
}
```

## 📝 Validation Checklist

Before calling ACE Hub:
- [ ] Valid class code for LOB
- [ ] Territory code populated
- [ ] Effective date is future date
- [ ] Coverage limits are within carrier guidelines
- [ ] All required addresses complete
- [ ] Exposure amount is reasonable
- [ ] Premium basis matches class code

## 🔄 Typical Workflow

1. **Collect Data** → Gather business information
2. **Build Request** → Create `AceHubRateRequest`
3. **Get Token** → Authenticate with OAuth
4. **Get Rates** → Call GetRate API
5. **Compare** → Review multiple carrier responses
6. **Select** → Choose best carrier/price
7. **Get Documents** → Retrieve quote documents
8. **Present** → Show to customer
9. **Bind** → (Separate bind API call if available)

## 🎨 Code Templates

### Template: Basic Request Shell
```csharp
public AceHubRateRequest CreateRequestTemplate(
    string businessName,
    string classCode,
    decimal exposure)
{
    return new AceHubRateRequest
    {
        SignonRq = CreateSignonRq(),
        InsuranceSvcRq = new InsuranceSvcRq
        {
            IRH_QuoteNo = GenerateQuoteNumber(),
            ComIRH_CarrierRequestExt = SelectCarriers(),
            CommlPkgPolicyQuoteInqRq = new CommlPkgPolicyQuoteInqRq
            {
                Producer = CreateProducer(),
                InsuredOrPrincipal = CreateInsured(businessName),
                Policy = CreatePolicy(),
                Location = CreateLocation(),
                GeneralLiabilityLineBusiness = CreateGL(classCode, exposure)
            }
        }
    };
}
```

## 📞 Quick Contacts

- **API Issues**: api-support@dyad.com
- **Credentialing**: credentials@dyad.com
- **Technical Support**: 1-800-XXX-XXXX
- **Documentation**: https://docs.acehub.dyad.com

---

**Pro Tip**: Start with a single carrier (Hartford or Travelers) to test your integration, then add multiple carriers once working.
