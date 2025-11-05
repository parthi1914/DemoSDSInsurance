using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.DTOs.LOB.Property;

namespace InsuranceWrapperApi.Domain.Mappers.LOB;

public class PropertyMapper : IPropertyMapper
{
    public PropertyQuoteRequest MapFromGeneric(GenericQuoteRequest request)
    {
        return new PropertyQuoteRequest
        {
            BusinessName = request.BusinessName ?? string.Empty,
            ContactName = request.ContactName ?? string.Empty,
            Email = request.Email ?? string.Empty,
            Phone = request.Phone ?? string.Empty,
            PropertyAddress = request.BusinessAddress ?? new Address(),
            EffectiveDate = request.EffectiveDate,
            
            // Property Specific mappings
            BuildingValue = request.BuildingValue ?? 0,
            BusinessPersonalPropertyValue = request.ContentsValue ?? 0,
            YearBuilt = request.YearBuilt ?? DateTime.Now.Year,
            NumberOfStories = 1, // Default
            TotalSquareFootage = 0, // Should be in additional data
            ConstructionType = request.ConstructionType ?? "Frame",
            RoofType = "Composition", // Default
            RoofAge = DateTime.Now.Year - (request.YearBuilt ?? DateTime.Now.Year),
            OccupancyType = request.IndustryType ?? string.Empty,
            
            // Protection features
            HasSprinklerSystem = request.HasSprinklers ?? false,
            SprinklerType = request.HasSprinklers == true ? "Full" : null,
            HasFireAlarm = request.HasAlarm ?? false,
            HasBurglarAlarm = request.HasAlarm ?? false,
            HasSecurityGuard = false,
            DistanceToFireStation = 5, // Default in miles
            DistanceToHydrant = 1000, // Default in feet
            FireProtectionClass = "5", // Default
            
            // Coverage details
            ValuationType = "ReplacementCost",
            BuildingDeductible = 2500,
            BPPDeductible = 2500,
            BusinessIncomeNeeded = false,
            EquipmentBreakdownNeeded = false,
            
            HasPriorClaims = false,
            PriorClaims = new List<PropertyClaim>(),
            
            AdditionalInfo = request.AdditionalData
        };
    }
}
