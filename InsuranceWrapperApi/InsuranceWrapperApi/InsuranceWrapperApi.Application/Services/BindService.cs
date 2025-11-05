using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.Interfaces;
using InsuranceWrapperApi.Domain.Enums;
using InsuranceWrapperApi.Domain.Factories;
using InsuranceWrapperApi.Domain.Mappers.Providers;
using InsuranceWrapperApi.Infrastructure.ApiClients;
using Microsoft.Extensions.Logging;

namespace InsuranceWrapperApi.Application.Services;

/// <summary>
/// Main orchestrator for bind operations
/// Coordinates provider mapping and API calls for policy binding
/// </summary>
public class BindService : IBindService
{
    private readonly ProviderMapperFactory _providerMapperFactory;
    private readonly IDyadApiClient _dyadClient;
    private readonly IHeraldApiClient _heraldClient;
    private readonly IZywaveApiClient _zywaveClient;
    private readonly ILogger<BindService> _logger;

    public BindService(
        ProviderMapperFactory providerMapperFactory,
        IDyadApiClient dyadClient,
        IHeraldApiClient heraldClient,
        IZywaveApiClient zywaveClient,
        ILogger<BindService> logger)
    {
        _providerMapperFactory = providerMapperFactory;
        _dyadClient = dyadClient;
        _heraldClient = heraldClient;
        _zywaveClient = zywaveClient;
        _logger = logger;
    }

    public async Task<GenericBindResponse> ProcessBindAsync(GenericBindRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Starting bind process for QuoteId: {QuoteId}, Provider: {Provider}", 
                request.QuoteId, request.Provider);

            // Validate request
            if (string.IsNullOrWhiteSpace(request.QuoteId))
            {
                _logger.LogWarning("Bind request missing QuoteId");
                return new GenericBindResponse
                {
                    Success = false,
                    Errors = new List<string> { "QuoteId is required for binding" }
                };
            }

            if (request.LineOfBusiness == LineOfBusiness.Unknown)
            {
                _logger.LogWarning("Bind request has unknown Line of Business");
                return new GenericBindResponse
                {
                    Success = false,
                    Errors = new List<string> { "Line of Business must be specified for binding" }
                };
            }

            if (request.Provider == ProviderType.Unknown)
            {
                _logger.LogWarning("Bind request has unknown Provider");
                return new GenericBindResponse
                {
                    Success = false,
                    Errors = new List<string> { "Provider must be specified for binding" }
                };
            }

            // Call provider-specific bind API
            var response = await CallProviderBindApi(request, cancellationToken);
            _logger.LogInformation("Received bind response from {Provider} for {LOB}", 
                request.Provider, request.LineOfBusiness);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing bind request");
            return new GenericBindResponse
            {
                Success = false,
                Errors = new List<string> { $"Error processing bind: {ex.Message}" }
            };
        }
    }

    private async Task<GenericBindResponse> CallProviderBindApi(GenericBindRequest request, CancellationToken cancellationToken)
    {
        return request.Provider switch
        {
            ProviderType.Dyad => await CallDyadBindApi(request, cancellationToken),
            ProviderType.Herald => await CallHeraldBindApi(request, cancellationToken),
            ProviderType.Zywave => await CallZywaveBindApi(request, cancellationToken),
            _ => throw new NotSupportedException($"Provider {request.Provider} not supported")
        };
    }

    private async Task<GenericBindResponse> CallDyadBindApi(GenericBindRequest request, CancellationToken cancellationToken)
    {
        var mapper = (IDyadBindMapper)_providerMapperFactory.GetBindMapper(ProviderType.Dyad);
        var dyadRequest = mapper.MapFromGenericBind(request, request.LineOfBusiness);
        
        _logger.LogDebug("Calling Dyad bind API for {LOB}", request.LineOfBusiness);
        var dyadResponse = await _dyadClient.BindPolicyAsync(dyadRequest, cancellationToken);
        
        return mapper.MapToGenericResponse(dyadResponse, request.LineOfBusiness);
    }

    private async Task<GenericBindResponse> CallHeraldBindApi(GenericBindRequest request, CancellationToken cancellationToken)
    {
        var mapper = (IHeraldBindMapper)_providerMapperFactory.GetBindMapper(ProviderType.Herald);
        var heraldRequest = mapper.MapFromGenericBind(request, request.LineOfBusiness);
        
        _logger.LogDebug("Calling Herald bind API for {LOB}", request.LineOfBusiness);
        var heraldResponse = await _heraldClient.BindPolicyAsync(heraldRequest, cancellationToken);
        
        return mapper.MapToGenericResponse(heraldResponse, request.LineOfBusiness);
    }

    private async Task<GenericBindResponse> CallZywaveBindApi(GenericBindRequest request, CancellationToken cancellationToken)
    {
        var mapper = (IZywaveBindMapper)_providerMapperFactory.GetBindMapper(ProviderType.Zywave);
        var zywaveRequest = mapper.MapFromGenericBind(request, request.LineOfBusiness);
        
        _logger.LogDebug("Calling Zywave bind API for {LOB}", request.LineOfBusiness);
        var zywaveResponse = await _zywaveClient.BindPolicyAsync(zywaveRequest, cancellationToken);
        
        return mapper.MapToGenericResponse(zywaveResponse, request.LineOfBusiness);
    }
}
