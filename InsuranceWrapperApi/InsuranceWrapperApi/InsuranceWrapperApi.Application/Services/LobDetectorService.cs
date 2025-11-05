using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.Interfaces;
using InsuranceWrapperApi.Domain.Enums;

namespace InsuranceWrapperApi.Application.Services;

/// <summary>
/// Service to automatically detect Line of Business from generic request
/// </summary>
public class LobDetectorService : ILobDetectorService
{
    public LineOfBusiness DetectLineOfBusiness(GenericQuoteRequest request)
    {
        // Priority order: Worker Comp > Flood > Property > GL
        
        // Worker Compensation indicators
        if (HasWorkerCompIndicators(request))
        {
            return LineOfBusiness.WorkerCompensation;
        }
        
        // Flood indicators
        if (HasFloodIndicators(request))
        {
            return LineOfBusiness.Flood;
        }
        
        // Property indicators
        if (HasPropertyIndicators(request))
        {
            return LineOfBusiness.Property;
        }
        
        // General Liability indicators (default fallback)
        if (HasGLIndicators(request))
        {
            return LineOfBusiness.GeneralLiability;
        }
        
        // If nothing matches, return Unknown
        return LineOfBusiness.Unknown;
    }

    private bool HasWorkerCompIndicators(GenericQuoteRequest request)
    {
        // Strong indicators for Worker Compensation
        return (request.PayrollByClass != null && request.PayrollByClass.Any()) ||
               !string.IsNullOrWhiteSpace(request.StateOfOperation);
    }

    private bool HasFloodIndicators(GenericQuoteRequest request)
    {
        // Strong indicators for Flood
        return !string.IsNullOrWhiteSpace(request.FloodZone) ||
               request.HasElevationCertificate.HasValue ||
               request.BaseFloodElevation.HasValue ||
               !string.IsNullOrWhiteSpace(request.BuildingOccupancyType);
    }

    private bool HasPropertyIndicators(GenericQuoteRequest request)
    {
        // Strong indicators for Property
        return request.BuildingValue.HasValue ||
               request.ContentsValue.HasValue ||
               !string.IsNullOrWhiteSpace(request.ConstructionType) ||
               request.YearBuilt.HasValue ||
               request.HasSprinklers.HasValue ||
               request.HasAlarm.HasValue;
    }

    private bool HasGLIndicators(GenericQuoteRequest request)
    {
        // Indicators for General Liability
        return !string.IsNullOrWhiteSpace(request.GLClassCode) ||
               !string.IsNullOrWhiteSpace(request.OperationsDescription) ||
               request.AnnualRevenue.HasValue;
    }
}
