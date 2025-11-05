using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.DTOs.Providers.Dyad;
using InsuranceWrapperApi.Application.DTOs.LOB.GL;
using InsuranceWrapperApi.Application.DTOs.LOB.Property;
using InsuranceWrapperApi.Application.DTOs.LOB.Flood;
using InsuranceWrapperApi.Application.DTOs.LOB.WorkerComp;
using InsuranceWrapperApi.Domain.Enums;

namespace InsuranceWrapperApi.Domain.Mappers.Providers;

public class DyadQuoteMapper : IDyadQuoteMapper
{
    public LineOfBusiness SupportedLob { get; set; }

    public DyadQuoteRequest MapFromLobDto(object lobDto, LineOfBusiness lob)
    {
        SupportedLob = lob;
        
        return lob switch
        {
            LineOfBusiness.GeneralLiability => MapFromGL((GLQuoteRequest)lobDto),
            LineOfBusiness.Property => MapFromProperty((PropertyQuoteRequest)lobDto),
            LineOfBusiness.Flood => MapFromFlood((FloodQuoteRequest)lobDto),
            LineOfBusiness.WorkerCompensation => MapFromWorkerComp((WorkerCompQuoteRequest)lobDto),
            _ => throw new NotSupportedException($"LOB {lob} not supported for Dyad")
        };
    }

    public GenericQuoteResponse MapToGenericResponse(DyadQuoteResponse response, LineOfBusiness lob)
    {
        if (!response.IsSuccessful)
        {
            return new GenericQuoteResponse
            {
                Success = false,
                Errors = response.ErrorMessages ?? new List<string> { "Quote request failed" },
                LineOfBusiness = lob,
                Provider = ProviderType.Dyad
            };
        }

        return new GenericQuoteResponse
        {
            Success = true,
            QuoteId = response.QuoteNumber,
            LineOfBusiness = lob,
            Provider = ProviderType.Dyad,
            Premium = response.Premium.BasePremium,
            Fees = response.Premium.Fees,
            TotalCost = response.Premium.TotalPremium,
            ExpirationDate = response.QuoteExpiration,
            Coverages = response.Coverages?.Select(c => new Coverage
            {
                CoverageType = c.CoverageName,
                Limit = c.Limit,
                Deductible = c.Deductible,
                Premium = c.Premium
            }).ToList(),
            Message = "Quote successfully retrieved from Dyad",
            ProviderSpecificData = response.ResponseData
        };
    }

    private DyadQuoteRequest MapFromGL(GLQuoteRequest glRequest)
    {
        return new DyadQuoteRequest
        {
            TransactionType = "Quote",
            Insured = new DyadInsured
            {
                Name = glRequest.BusinessName,
                ContactPerson = glRequest.ContactName,
                EmailAddress = glRequest.Email,
                PhoneNumber = glRequest.Phone,
                Address = MapAddress(glRequest.BusinessAddress)
            },
            Policy = new DyadPolicy
            {
                LineOfBusiness = "GeneralLiability",
                EffectiveDate = glRequest.EffectiveDate,
                ExpirationDate = glRequest.EffectiveDate.AddYears(1),
                PolicyTerm = "12"
            },
            Coverages = new DyadCoverages
            {
                CoverageList = new List<DyadCoverage>
                {
                    new() { CoverageCode = "GL_AGGREGATE", Limit = glRequest.GeneralAggregateLimitRequested },
                    new() { CoverageCode = "GL_OCCURRENCE", Limit = glRequest.PerOccurrenceLimitRequested },
                    new() { CoverageCode = "GL_PERSONAL_ADV", Limit = glRequest.PersonalAndAdvInjuryLimitRequested },
                    new() { CoverageCode = "GL_PRODUCTS_OPS", Limit = glRequest.ProductsCompletedOpsAggregateRequested }
                }
            },
            AdditionalData = new Dictionary<string, object>
            {
                { "ClassCode", glRequest.ClassCode },
                { "AnnualRevenue", glRequest.AnnualRevenue },
                { "NumberOfEmployees", glRequest.NumberOfEmployees }
            }
        };
    }

    private DyadQuoteRequest MapFromProperty(PropertyQuoteRequest propRequest)
    {
        return new DyadQuoteRequest
        {
            TransactionType = "Quote",
            Insured = new DyadInsured
            {
                Name = propRequest.BusinessName,
                ContactPerson = propRequest.ContactName,
                EmailAddress = propRequest.Email,
                PhoneNumber = propRequest.Phone,
                Address = MapAddress(propRequest.PropertyAddress)
            },
            Policy = new DyadPolicy
            {
                LineOfBusiness = "Property",
                EffectiveDate = propRequest.EffectiveDate,
                ExpirationDate = propRequest.EffectiveDate.AddYears(1),
                PolicyTerm = "12"
            },
            Coverages = new DyadCoverages
            {
                CoverageList = new List<DyadCoverage>
                {
                    new() { CoverageCode = "BUILDING", Limit = propRequest.BuildingValue, Deductible = propRequest.BuildingDeductible },
                    new() { CoverageCode = "BPP", Limit = propRequest.BusinessPersonalPropertyValue, Deductible = propRequest.BPPDeductible }
                }
            },
            AdditionalData = new Dictionary<string, object>
            {
                { "YearBuilt", propRequest.YearBuilt },
                { "ConstructionType", propRequest.ConstructionType },
                { "HasSprinklers", propRequest.HasSprinklerSystem }
            }
        };
    }

    private DyadQuoteRequest MapFromFlood(FloodQuoteRequest floodRequest)
    {
        return new DyadQuoteRequest
        {
            TransactionType = "Quote",
            Insured = new DyadInsured
            {
                Name = floodRequest.PropertyOwnerName,
                ContactPerson = floodRequest.ContactName,
                EmailAddress = floodRequest.Email,
                PhoneNumber = floodRequest.Phone,
                Address = MapAddress(floodRequest.PropertyAddress)
            },
            Policy = new DyadPolicy
            {
                LineOfBusiness = "Flood",
                EffectiveDate = floodRequest.EffectiveDate,
                ExpirationDate = floodRequest.EffectiveDate.AddYears(1),
                PolicyTerm = "12"
            },
            Coverages = new DyadCoverages
            {
                CoverageList = new List<DyadCoverage>
                {
                    new() { CoverageCode = "FLOOD_BUILDING", Limit = floodRequest.BuildingCoverageAmount, Deductible = floodRequest.BuildingDeductible },
                    new() { CoverageCode = "FLOOD_CONTENTS", Limit = floodRequest.ContentsCoverageAmount, Deductible = floodRequest.ContentsDeductible }
                }
            },
            AdditionalData = new Dictionary<string, object>
            {
                { "FloodZone", floodRequest.FloodZone },
                { "HasElevationCert", floodRequest.HasElevationCertificate }
            }
        };
    }

    private DyadQuoteRequest MapFromWorkerComp(WorkerCompQuoteRequest wcRequest)
    {
        return new DyadQuoteRequest
        {
            TransactionType = "Quote",
            Insured = new DyadInsured
            {
                Name = wcRequest.BusinessName,
                ContactPerson = wcRequest.ContactName,
                EmailAddress = wcRequest.Email,
                PhoneNumber = wcRequest.Phone,
                Address = MapAddress(wcRequest.BusinessAddress)
            },
            Policy = new DyadPolicy
            {
                LineOfBusiness = "WorkersCompensation",
                EffectiveDate = wcRequest.EffectiveDate,
                ExpirationDate = wcRequest.EffectiveDate.AddYears(1),
                PolicyTerm = "12"
            },
            Coverages = new DyadCoverages
            {
                CoverageList = new List<DyadCoverage>
                {
                    new() { CoverageCode = "WC_STATUTORY" },
                    new() { CoverageCode = "EL_EACH_ACCIDENT", Limit = wcRequest.ELEachAccidentLimit },
                    new() { CoverageCode = "EL_DISEASE_EMPLOYEE", Limit = wcRequest.ELDiseaseEachEmployeeLimit },
                    new() { CoverageCode = "EL_DISEASE_POLICY", Limit = wcRequest.ELDiseasePolicyLimit }
                }
            },
            AdditionalData = new Dictionary<string, object>
            {
                { "StateOfOperation", wcRequest.StateOfOperation },
                { "TotalPayroll", wcRequest.TotalAnnualPayroll },
                { "PayrollClasses", wcRequest.PayrollByClass }
            }
        };
    }

    private DyadAddress MapAddress(Address address)
    {
        return new DyadAddress
        {
            AddressLine1 = address.Street ?? string.Empty,
            City = address.City ?? string.Empty,
            StateCode = address.State ?? string.Empty,
            PostalCode = address.ZipCode ?? string.Empty,
            CountyName = address.County ?? string.Empty
        };
    }
}

public class DyadBindMapper : IDyadBindMapper
{
    public LineOfBusiness SupportedLob { get; set; }

    public DyadBindRequest MapFromGenericBind(GenericBindRequest request, LineOfBusiness lob)
    {
        SupportedLob = lob;
        
        return new DyadBindRequest
        {
            QuoteNumber = request.QuoteId,
            Payment = new DyadPayment
            {
                PaymentType = request.Payment?.PaymentMethod ?? "CreditCard",
                Amount = request.Payment?.Amount ?? 0,
                PaymentDetails = new Dictionary<string, string>()
            }
        };
    }

    public GenericBindResponse MapToGenericResponse(DyadBindResponse response, LineOfBusiness lob)
    {
        if (!response.IsSuccessful)
        {
            return new GenericBindResponse
            {
                Success = false,
                Errors = response.ErrorMessages ?? new List<string> { "Bind request failed" },
                LineOfBusiness = lob,
                Provider = ProviderType.Dyad
            };
        }

        return new GenericBindResponse
        {
            Success = true,
            PolicyNumber = response.PolicyNumber,
            LineOfBusiness = lob,
            Provider = ProviderType.Dyad,
            EffectiveDate = response.EffectiveDate,
            ExpirationDate = response.ExpirationDate,
            BoundPremium = response.BoundPremium,
            Message = "Policy successfully bound with Dyad"
        };
    }
}
