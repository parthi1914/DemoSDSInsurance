using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.DTOs.LOB.Flood;

namespace InsuranceWrapperApi.Domain.Mappers.LOB;

public class FloodMapper : IFloodMapper
{
    public FloodQuoteRequest MapFromGeneric(GenericQuoteRequest request)
    {
        return new FloodQuoteRequest
        {
            PropertyOwnerName = request.BusinessName ?? string.Empty,
            ContactName = request.ContactName ?? string.Empty,
            Email = request.Email ?? string.Empty,
            Phone = request.Phone ?? string.Empty,
            PropertyAddress = request.BusinessAddress ?? new Address(),
            EffectiveDate = request.EffectiveDate,
            
            // Flood Specific mappings
            FloodZone = request.FloodZone ?? string.Empty,
            CommunityNumber = string.Empty, // Should be in additional data
            PanelNumber = string.Empty, // Should be in additional data
            FIRMDate = null,
            
            // Building information
            OccupancyType = request.BuildingOccupancyType ?? request.IndustryType ?? string.Empty,
            FoundationType = "Slab", // Default
            NumberOfFloors = 1,
            YearBuilt = request.YearBuilt ?? DateTime.Now.Year,
            HasBasement = false,
            HasEnclosedArea = false,
            
            // Elevation information
            HasElevationCertificate = request.HasElevationCertificate ?? false,
            LowestFloorElevation = null,
            BaseFloodElevation = request.BaseFloodElevation,
            ElevationDifference = null,
            ElevationCertificateDate = null,
            
            // Coverage
            BuildingCoverageAmount = request.BuildingValue ?? 0,
            ContentsCoverageAmount = request.ContentsValue ?? 0,
            BuildingDeductible = 5000,
            ContentsDeductible = 5000,
            
            // Additional
            IsPreferredRiskPolicy = false,
            IsCondo = false,
            IsNonProfit = false,
            
            AdditionalInfo = request.AdditionalData
        };
    }
}
