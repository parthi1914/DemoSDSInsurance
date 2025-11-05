using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.DTOs.LOB.GL;

namespace InsuranceWrapperApi.Domain.Mappers.LOB;

public class GLMapper : IGLMapper
{
    public GLQuoteRequest MapFromGeneric(GenericQuoteRequest request)
    {
        return new GLQuoteRequest
        {
            BusinessName = request.BusinessName ?? string.Empty,
            ContactName = request.ContactName ?? string.Empty,
            Email = request.Email ?? string.Empty,
            Phone = request.Phone ?? string.Empty,
            BusinessAddress = request.BusinessAddress ?? new Address(),
            EffectiveDate = request.EffectiveDate,
            
            // GL Specific mappings
            ClassCode = request.GLClassCode ?? string.Empty,
            ClassDescription = request.IndustryType ?? string.Empty,
            OperationsDescription = request.OperationsDescription ?? string.Empty,
            AnnualRevenue = request.AnnualRevenue ?? 0,
            NumberOfEmployees = request.NumberOfEmployees ?? 0,
            YearsInBusiness = request.YearsInBusiness ?? 0,
            
            // Default coverage limits (can be overridden by additional data)
            GeneralAggregateLimitRequested = 2_000_000,
            PerOccurrenceLimitRequested = 1_000_000,
            PersonalAndAdvInjuryLimitRequested = 1_000_000,
            ProductsCompletedOpsAggregateRequested = 2_000_000,
            MedicalExpenseLimitRequested = 5_000,
            DamageToRentedPremisesRequested = 100_000,
            
            HasPriorClaims = false,
            PriorClaims = new List<GLClaim>(),
            
            AdditionalInfo = request.AdditionalData
        };
    }
}
