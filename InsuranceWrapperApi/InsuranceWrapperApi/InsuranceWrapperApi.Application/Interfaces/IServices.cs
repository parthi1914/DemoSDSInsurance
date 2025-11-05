using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Domain.Enums;

namespace InsuranceWrapperApi.Application.Interfaces;

/// <summary>
/// Service to detect Line of Business from generic request
/// </summary>
public interface ILobDetectorService
{
    LineOfBusiness DetectLineOfBusiness(GenericQuoteRequest request);
}

/// <summary>
/// Main orchestrator for quote operations
/// </summary>
public interface IQuoteService
{
    Task<GenericQuoteResponse> ProcessQuoteAsync(GenericQuoteRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Main orchestrator for bind operations
/// </summary>
public interface IBindService
{
    Task<GenericBindResponse> ProcessBindAsync(GenericBindRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Service to determine which provider to use
/// </summary>
public interface IProviderSelectionService
{
    ProviderType SelectProvider(LineOfBusiness lob, GenericQuoteRequest request);
}
