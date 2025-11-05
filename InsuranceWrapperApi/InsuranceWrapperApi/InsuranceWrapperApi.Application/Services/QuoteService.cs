using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.Interfaces;
using InsuranceWrapperApi.Domain.Enums;
using InsuranceWrapperApi.Domain.Factories;
using InsuranceWrapperApi.Domain.Mappers.LOB;
using InsuranceWrapperApi.Domain.Mappers.Providers;
using InsuranceWrapperApi.Infrastructure.ApiClients;
using Microsoft.Extensions.Logging;

namespace InsuranceWrapperApi.Application.Services;

/// <summary>
/// Main orchestrator for quote operations
/// Coordinates LOB detection, mapping, provider selection, and API calls
/// </summary>
public class QuoteService : IQuoteService
{
    private readonly ILobDetectorService _lobDetector;
    private readonly IProviderSelectionService _providerSelector;
    private readonly LobMapperFactory _lobMapperFactory;
    private readonly ProviderMapperFactory _providerMapperFactory;
    private readonly IDyadApiClient _dyadClient;
    private readonly IHeraldApiClient _heraldClient;
    private readonly IZywaveApiClient _zywaveClient;
    private readonly ILogger<QuoteService> _logger;

    public QuoteService(
        ILobDetectorService lobDetector,
        IProviderSelectionService providerSelector,
        LobMapperFactory lobMapperFactory,
        ProviderMapperFactory providerMapperFactory,
        IDyadApiClient dyadClient,
        IHeraldApiClient heraldClient,
        IZywaveApiClient zywaveClient,
        ILogger<QuoteService> logger)
    {
        _lobDetector = lobDetector;
        _providerSelector = providerSelector;
        _lobMapperFactory = lobMapperFactory;
        _providerMapperFactory = providerMapperFactory;
        _dyadClient = dyadClient;
        _heraldClient = heraldClient;
        _zywaveClient = zywaveClient;
        _logger = logger;
    }

    public async Task<GenericQuoteResponse> ProcessQuoteAsync(GenericQuoteRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Starting quote process for business: {BusinessName}", request.BusinessName);

            // Step 1: Detect Line of Business
            var lob = _lobDetector.DetectLineOfBusiness(request);
            _logger.LogInformation("Detected Line of Business: {LOB}", lob);

            if (lob == LineOfBusiness.Unknown)
            {
                _logger.LogWarning("Unable to determine Line of Business from request");
                return new GenericQuoteResponse
                {
                    Success = false,
                    Errors = new List<string> { "Unable to determine Line of Business from provided information" }
                };
            }

            // Step 2: Map to LOB-specific DTO
            var lobDto = MapToLobDto(request, lob);
            _logger.LogInformation("Mapped to {LOB} DTO", lob);

            // Step 3: Select Provider
            var provider = _providerSelector.SelectProvider(lob, request);
            _logger.LogInformation("Selected Provider: {Provider}", provider);

            // Step 4: Map to Provider-specific DTO and call API
            var response = await CallProviderQuoteApi(lobDto, lob, provider, cancellationToken);
            _logger.LogInformation("Received response from {Provider} for {LOB}", provider, lob);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing quote request");
            return new GenericQuoteResponse
            {
                Success = false,
                Errors = new List<string> { $"Error processing quote: {ex.Message}" }
            };
        }
    }

    private object MapToLobDto(GenericQuoteRequest request, LineOfBusiness lob)
    {
        return lob switch
        {
            LineOfBusiness.GeneralLiability => ((IGLMapper)_lobMapperFactory.GetMapper(lob)).MapFromGeneric(request),
            LineOfBusiness.Property => ((IPropertyMapper)_lobMapperFactory.GetMapper(lob)).MapFromGeneric(request),
            LineOfBusiness.Flood => ((IFloodMapper)_lobMapperFactory.GetMapper(lob)).MapFromGeneric(request),
            LineOfBusiness.WorkerCompensation => ((IWorkerCompMapper)_lobMapperFactory.GetMapper(lob)).MapFromGeneric(request),
            _ => throw new NotSupportedException($"LOB {lob} not supported")
        };
    }

    private async Task<GenericQuoteResponse> CallProviderQuoteApi(object lobDto, LineOfBusiness lob, ProviderType provider, CancellationToken cancellationToken)
    {
        return provider switch
        {
            ProviderType.Dyad => await CallDyadQuoteApi(lobDto, lob, cancellationToken),
            ProviderType.Herald => await CallHeraldQuoteApi(lobDto, lob, cancellationToken),
            ProviderType.Zywave => await CallZywaveQuoteApi(lobDto, lob, cancellationToken),
            _ => throw new NotSupportedException($"Provider {provider} not supported")
        };
    }

    private async Task<GenericQuoteResponse> CallDyadQuoteApi(object lobDto, LineOfBusiness lob, CancellationToken cancellationToken)
    {
        var mapper = (IDyadQuoteMapper)_providerMapperFactory.GetQuoteMapper(ProviderType.Dyad);
        var dyadRequest = mapper.MapFromLobDto(lobDto, lob);
        
        _logger.LogDebug("Calling Dyad API for {LOB}", lob);
        var dyadResponse = await _dyadClient.GetQuoteAsync(dyadRequest, cancellationToken);
        
        return mapper.MapToGenericResponse(dyadResponse, lob);
    }

    private async Task<GenericQuoteResponse> CallHeraldQuoteApi(object lobDto, LineOfBusiness lob, CancellationToken cancellationToken)
    {
        var mapper = (IHeraldQuoteMapper)_providerMapperFactory.GetQuoteMapper(ProviderType.Herald);
        var heraldRequest = mapper.MapFromLobDto(lobDto, lob);
        
        _logger.LogDebug("Calling Herald API for {LOB}", lob);
        var heraldResponse = await _heraldClient.GetQuoteAsync(heraldRequest, cancellationToken);
        
        return mapper.MapToGenericResponse(heraldResponse, lob);
    }

    private async Task<GenericQuoteResponse> CallZywaveQuoteApi(object lobDto, LineOfBusiness lob, CancellationToken cancellationToken)
    {
        var mapper = (IZywaveQuoteMapper)_providerMapperFactory.GetQuoteMapper(ProviderType.Zywave);
        var zywaveRequest = mapper.MapFromLobDto(lobDto, lob);
        
        _logger.LogDebug("Calling Zywave API for {LOB}", lob);
        var zywaveResponse = await _zywaveClient.GetQuoteAsync(zywaveRequest, cancellationToken);
        
        return mapper.MapToGenericResponse(zywaveResponse, lob);
    }
}
