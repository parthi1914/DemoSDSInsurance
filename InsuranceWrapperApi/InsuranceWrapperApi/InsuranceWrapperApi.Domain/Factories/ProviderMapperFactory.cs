using InsuranceWrapperApi.Domain.Enums;
using InsuranceWrapperApi.Domain.Mappers.Providers;

namespace InsuranceWrapperApi.Domain.Factories;

/// <summary>
/// Factory to create appropriate provider mapper based on provider and operation type
/// </summary>
public class ProviderMapperFactory
{
    private readonly IDyadQuoteMapper _dyadQuoteMapper;
    private readonly IDyadBindMapper _dyadBindMapper;
    private readonly IHeraldQuoteMapper _heraldQuoteMapper;
    private readonly IHeraldBindMapper _heraldBindMapper;
    private readonly IZywaveQuoteMapper _zywaveQuoteMapper;
    private readonly IZywaveBindMapper _zywaveBindMapper;

    public ProviderMapperFactory(
        IDyadQuoteMapper dyadQuoteMapper,
        IDyadBindMapper dyadBindMapper,
        IHeraldQuoteMapper heraldQuoteMapper,
        IHeraldBindMapper heraldBindMapper,
        IZywaveQuoteMapper zywaveQuoteMapper,
        IZywaveBindMapper zywaveBindMapper)
    {
        _dyadQuoteMapper = dyadQuoteMapper;
        _dyadBindMapper = dyadBindMapper;
        _heraldQuoteMapper = heraldQuoteMapper;
        _heraldBindMapper = heraldBindMapper;
        _zywaveQuoteMapper = zywaveQuoteMapper;
        _zywaveBindMapper = zywaveBindMapper;
    }

    public object GetQuoteMapper(ProviderType provider)
    {
        return provider switch
        {
            ProviderType.Dyad => _dyadQuoteMapper,
            ProviderType.Herald => _heraldQuoteMapper,
            ProviderType.Zywave => _zywaveQuoteMapper,
            _ => throw new NotSupportedException($"No quote mapper available for provider: {provider}")
        };
    }

    public object GetBindMapper(ProviderType provider)
    {
        return provider switch
        {
            ProviderType.Dyad => _dyadBindMapper,
            ProviderType.Herald => _heraldBindMapper,
            ProviderType.Zywave => _zywaveBindMapper,
            _ => throw new NotSupportedException($"No bind mapper available for provider: {provider}")
        };
    }
}
