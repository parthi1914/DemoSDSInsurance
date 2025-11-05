using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.DTOs.Providers.Dyad;
using InsuranceWrapperApi.Application.DTOs.Providers.Herald;
using InsuranceWrapperApi.Application.DTOs.Providers.Zywave;
using InsuranceWrapperApi.Domain.Enums;

namespace InsuranceWrapperApi.Domain.Mappers.Providers;

/// <summary>
/// Base interface for provider mappers
/// </summary>
public interface IProviderMapper
{
    LineOfBusiness SupportedLob { get; }
}

/// <summary>
/// Dyad provider mapper for quote requests
/// </summary>
public interface IDyadQuoteMapper : IProviderMapper
{
    DyadQuoteRequest MapFromLobDto(object lobDto, LineOfBusiness lob);
    GenericQuoteResponse MapToGenericResponse(DyadQuoteResponse response, LineOfBusiness lob);
}

/// <summary>
/// Dyad provider mapper for bind requests
/// </summary>
public interface IDyadBindMapper : IProviderMapper
{
    DyadBindRequest MapFromGenericBind(GenericBindRequest request, LineOfBusiness lob);
    GenericBindResponse MapToGenericResponse(DyadBindResponse response, LineOfBusiness lob);
}

/// <summary>
/// Herald provider mapper for quote requests
/// </summary>
public interface IHeraldQuoteMapper : IProviderMapper
{
    HeraldQuoteRequest MapFromLobDto(object lobDto, LineOfBusiness lob);
    GenericQuoteResponse MapToGenericResponse(HeraldQuoteResponse response, LineOfBusiness lob);
}

/// <summary>
/// Herald provider mapper for bind requests
/// </summary>
public interface IHeraldBindMapper : IProviderMapper
{
    HeraldBindRequest MapFromGenericBind(GenericBindRequest request, LineOfBusiness lob);
    GenericBindResponse MapToGenericResponse(HeraldBindResponse response, LineOfBusiness lob);
}

/// <summary>
/// Zywave provider mapper for quote requests
/// </summary>
public interface IZywaveQuoteMapper : IProviderMapper
{
    ZywaveQuoteRequest MapFromLobDto(object lobDto, LineOfBusiness lob);
    GenericQuoteResponse MapToGenericResponse(ZywaveQuoteResponse response, LineOfBusiness lob);
}

/// <summary>
/// Zywave provider mapper for bind requests
/// </summary>
public interface IZywaveBindMapper : IProviderMapper
{
    ZywaveBindRequest MapFromGenericBind(GenericBindRequest request, LineOfBusiness lob);
    GenericBindResponse MapToGenericResponse(ZywaveBindResponse response, LineOfBusiness lob);
}
