using InsuranceWrapperApi.Domain.Enums;
using InsuranceWrapperApi.Domain.Mappers.LOB;

namespace InsuranceWrapperApi.Domain.Factories;

/// <summary>
/// Factory to create appropriate LOB mapper based on Line of Business
/// </summary>
public class LobMapperFactory
{
    private readonly IGLMapper _glMapper;
    private readonly IPropertyMapper _propertyMapper;
    private readonly IFloodMapper _floodMapper;
    private readonly IWorkerCompMapper _workerCompMapper;

    public LobMapperFactory(
        IGLMapper glMapper,
        IPropertyMapper propertyMapper,
        IFloodMapper floodMapper,
        IWorkerCompMapper workerCompMapper)
    {
        _glMapper = glMapper;
        _propertyMapper = propertyMapper;
        _floodMapper = floodMapper;
        _workerCompMapper = workerCompMapper;
    }

    public object GetMapper(LineOfBusiness lob)
    {
        return lob switch
        {
            LineOfBusiness.GeneralLiability => _glMapper,
            LineOfBusiness.Property => _propertyMapper,
            LineOfBusiness.Flood => _floodMapper,
            LineOfBusiness.WorkerCompensation => _workerCompMapper,
            _ => throw new NotSupportedException($"No mapper available for LOB: {lob}")
        };
    }
}
