using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.DTOs.LOB.GL;
using InsuranceWrapperApi.Application.DTOs.LOB.Property;
using InsuranceWrapperApi.Application.DTOs.LOB.Flood;
using InsuranceWrapperApi.Application.DTOs.LOB.WorkerComp;

namespace InsuranceWrapperApi.Domain.Mappers.LOB;

/// <summary>
/// Base interface for LOB mappers
/// </summary>
public interface ILobMapper<TLobDto>
{
    TLobDto MapFromGeneric(GenericQuoteRequest request);
}

/// <summary>
/// General Liability mapper
/// </summary>
public interface IGLMapper : ILobMapper<GLQuoteRequest>
{
}

/// <summary>
/// Property mapper
/// </summary>
public interface IPropertyMapper : ILobMapper<PropertyQuoteRequest>
{
}

/// <summary>
/// Flood mapper
/// </summary>
public interface IFloodMapper : ILobMapper<FloodQuoteRequest>
{
}

/// <summary>
/// Worker Compensation mapper
/// </summary>
public interface IWorkerCompMapper : ILobMapper<WorkerCompQuoteRequest>
{
}
