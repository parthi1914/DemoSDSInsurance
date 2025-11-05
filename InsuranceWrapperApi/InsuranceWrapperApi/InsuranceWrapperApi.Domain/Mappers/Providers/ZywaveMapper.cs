using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.DTOs.Providers.Zywave;
using InsuranceWrapperApi.Application.DTOs.LOB.GL;
using InsuranceWrapperApi.Application.DTOs.LOB.Property;
using InsuranceWrapperApi.Application.DTOs.LOB.Flood;
using InsuranceWrapperApi.Application.DTOs.LOB.WorkerComp;
using InsuranceWrapperApi.Domain.Enums;

namespace InsuranceWrapperApi.Domain.Mappers.Providers;

public class ZywaveQuoteMapper : IZywaveQuoteMapper
{
    public LineOfBusiness SupportedLob { get; set; }

    public ZywaveQuoteRequest MapFromLobDto(object lobDto, LineOfBusiness lob)
    {
        SupportedLob = lob;
        var request = new ZywaveQuoteRequest { ActionType = "QUOTE" };

        switch (lob)
        {
            case LineOfBusiness.GeneralLiability:
                var glDto = (GLQuoteRequest)lobDto;
                request.Entity = MapEntity(glDto.BusinessName, glDto.ContactName, glDto.Email, glDto.Phone, glDto.BusinessAddress);
                request.PolicyInfo = new ZywavePolicyInfo 
                { 
                    PolicyType = "GL", 
                    EffectiveDate = glDto.EffectiveDate,
                    ExpirationDate = glDto.EffectiveDate.AddYears(1)
                };
                break;
            case LineOfBusiness.Property:
                var propDto = (PropertyQuoteRequest)lobDto;
                request.Entity = MapEntity(propDto.BusinessName, propDto.ContactName, propDto.Email, propDto.Phone, propDto.PropertyAddress);
                request.PolicyInfo = new ZywavePolicyInfo 
                { 
                    PolicyType = "PROPERTY", 
                    EffectiveDate = propDto.EffectiveDate,
                    ExpirationDate = propDto.EffectiveDate.AddYears(1)
                };
                break;
            case LineOfBusiness.Flood:
                var floodDto = (FloodQuoteRequest)lobDto;
                request.Entity = MapEntity(floodDto.PropertyOwnerName, floodDto.ContactName, floodDto.Email, floodDto.Phone, floodDto.PropertyAddress);
                request.PolicyInfo = new ZywavePolicyInfo 
                { 
                    PolicyType = "FLOOD", 
                    EffectiveDate = floodDto.EffectiveDate,
                    ExpirationDate = floodDto.EffectiveDate.AddYears(1)
                };
                break;
            case LineOfBusiness.WorkerCompensation:
                var wcDto = (WorkerCompQuoteRequest)lobDto;
                request.Entity = MapEntity(wcDto.BusinessName, wcDto.ContactName, wcDto.Email, wcDto.Phone, wcDto.BusinessAddress);
                request.PolicyInfo = new ZywavePolicyInfo 
                { 
                    PolicyType = "WC", 
                    EffectiveDate = wcDto.EffectiveDate,
                    ExpirationDate = wcDto.EffectiveDate.AddYears(1)
                };
                break;
        }

        return request;
    }

    public GenericQuoteResponse MapToGenericResponse(ZywaveQuoteResponse response, LineOfBusiness lob)
    {
        if (response.ResponseStatus != "SUCCESS")
        {
            return new GenericQuoteResponse
            {
                Success = false,
                Errors = response.Messages?.Where(m => m.Severity == "ERROR").Select(m => m.MessageText).ToList() 
                    ?? new List<string> { "Quote failed" },
                LineOfBusiness = lob,
                Provider = ProviderType.Zywave
            };
        }

        return new GenericQuoteResponse
        {
            Success = true,
            QuoteId = response.QuoteId,
            LineOfBusiness = lob,
            Provider = ProviderType.Zywave,
            Premium = response.PremiumBreakdown.PolicyPremium,
            Fees = response.PremiumBreakdown.SurchargesAndFees,
            TotalCost = response.PremiumBreakdown.TotalAmount,
            ExpirationDate = response.ExpirationTimestamp,
            Coverages = response.Coverages?.Select(c => new Coverage
            {
                CoverageType = c.Description,
                Limit = c.LimitAmount,
                Deductible = c.DeductibleAmount,
                Premium = c.PremiumAmount
            }).ToList(),
            Message = "Quote successfully retrieved from Zywave"
        };
    }

    private ZywaveEntity MapEntity(string name, string contact, string email, string phone, Address address)
    {
        return new ZywaveEntity
        {
            EntityName = name,
            ContactName = contact,
            EmailAddress = email,
            PhoneNumber = phone,
            MailingAddress = new ZywaveAddress
            {
                Line1 = address.Street ?? string.Empty,
                City = address.City ?? string.Empty,
                StateProvince = address.State ?? string.Empty,
                PostalCode = address.ZipCode ?? string.Empty
            }
        };
    }
}

public class ZywaveBindMapper : IZywaveBindMapper
{
    public LineOfBusiness SupportedLob { get; set; }

    public ZywaveBindRequest MapFromGenericBind(GenericBindRequest request, LineOfBusiness lob)
    {
        SupportedLob = lob;
        
        return new ZywaveBindRequest
        {
            QuoteId = request.QuoteId,
            ActionType = "BIND",
            PaymentInfo = new ZywavePaymentInfo
            {
                PaymentMethod = request.Payment?.PaymentMethod ?? "CREDIT_CARD",
                PaymentAmount = request.Payment?.Amount ?? 0
            }
        };
    }

    public GenericBindResponse MapToGenericResponse(ZywaveBindResponse response, LineOfBusiness lob)
    {
        if (response.ResponseStatus != "SUCCESS")
        {
            return new GenericBindResponse
            {
                Success = false,
                Errors = response.Messages?.Where(m => m.Severity == "ERROR").Select(m => m.MessageText).ToList() 
                    ?? new List<string> { "Bind failed" },
                LineOfBusiness = lob,
                Provider = ProviderType.Zywave
            };
        }

        return new GenericBindResponse
        {
            Success = true,
            PolicyNumber = response.PolicyNumber,
            LineOfBusiness = lob,
            Provider = ProviderType.Zywave,
            EffectiveDate = response.PolicyEffectiveDate,
            ExpirationDate = response.PolicyExpirationDate,
            BoundPremium = response.TotalPremium,
            Message = "Policy successfully bound with Zywave"
        };
    }
}
