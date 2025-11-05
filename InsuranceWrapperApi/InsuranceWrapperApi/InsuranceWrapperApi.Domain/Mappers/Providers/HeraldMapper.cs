using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.DTOs.Providers.Herald;
using InsuranceWrapperApi.Application.DTOs.LOB.GL;
using InsuranceWrapperApi.Application.DTOs.LOB.Property;
using InsuranceWrapperApi.Application.DTOs.LOB.Flood;
using InsuranceWrapperApi.Application.DTOs.LOB.WorkerComp;
using InsuranceWrapperApi.Domain.Enums;

namespace InsuranceWrapperApi.Domain.Mappers.Providers;

public class HeraldQuoteMapper : IHeraldQuoteMapper
{
    public LineOfBusiness SupportedLob { get; set; }

    public HeraldQuoteRequest MapFromLobDto(object lobDto, LineOfBusiness lob)
    {
        SupportedLob = lob;
        var request = new HeraldQuoteRequest { Operation = "GetQuote" };

        switch (lob)
        {
            case LineOfBusiness.GeneralLiability:
                var glDto = (GLQuoteRequest)lobDto;
                request.Applicant = MapApplicant(glDto.BusinessName, glDto.ContactName, glDto.Email, glDto.Phone, glDto.BusinessAddress);
                request.Coverage = new HeraldCoverage { ProductLine = "GL", PolicyStartDate = glDto.EffectiveDate };
                break;
            case LineOfBusiness.Property:
                var propDto = (PropertyQuoteRequest)lobDto;
                request.Applicant = MapApplicant(propDto.BusinessName, propDto.ContactName, propDto.Email, propDto.Phone, propDto.PropertyAddress);
                request.Coverage = new HeraldCoverage { ProductLine = "PROPERTY", PolicyStartDate = propDto.EffectiveDate };
                break;
            case LineOfBusiness.Flood:
                var floodDto = (FloodQuoteRequest)lobDto;
                request.Applicant = MapApplicant(floodDto.PropertyOwnerName, floodDto.ContactName, floodDto.Email, floodDto.Phone, floodDto.PropertyAddress);
                request.Coverage = new HeraldCoverage { ProductLine = "FLOOD", PolicyStartDate = floodDto.EffectiveDate };
                break;
            case LineOfBusiness.WorkerCompensation:
                var wcDto = (WorkerCompQuoteRequest)lobDto;
                request.Applicant = MapApplicant(wcDto.BusinessName, wcDto.ContactName, wcDto.Email, wcDto.Phone, wcDto.BusinessAddress);
                request.Coverage = new HeraldCoverage { ProductLine = "WC", PolicyStartDate = wcDto.EffectiveDate };
                break;
        }

        return request;
    }

    public GenericQuoteResponse MapToGenericResponse(HeraldQuoteResponse response, LineOfBusiness lob)
    {
        if (response.Status != "Success")
        {
            return new GenericQuoteResponse
            {
                Success = false,
                Errors = response.Errors?.Select(e => e.Message).ToList() ?? new List<string> { "Quote failed" },
                LineOfBusiness = lob,
                Provider = ProviderType.Herald
            };
        }

        return new GenericQuoteResponse
        {
            Success = true,
            QuoteId = response.QuoteReference,
            LineOfBusiness = lob,
            Provider = ProviderType.Herald,
            Premium = response.PremiumDetail.Base,
            Fees = response.PremiumDetail.Fee,
            TotalCost = response.PremiumDetail.Total,
            ExpirationDate = response.ValidUntil,
            Coverages = response.CoverageDetails?.Select(c => new Coverage
            {
                CoverageType = c.CoverageType,
                Limit = c.Limit,
                Deductible = c.Deductible,
                Premium = c.Premium
            }).ToList(),
            Message = "Quote successfully retrieved from Herald"
        };
    }

    private HeraldApplicant MapApplicant(string name, string contact, string email, string phone, Address address)
    {
        return new HeraldApplicant
        {
            BusinessName = name,
            PrimaryContact = contact,
            Email = email,
            Phone = phone,
            Location = new HeraldLocation
            {
                Street = address.Street ?? string.Empty,
                City = address.City ?? string.Empty,
                State = address.State ?? string.Empty,
                Zip = address.ZipCode ?? string.Empty
            }
        };
    }
}

public class HeraldBindMapper : IHeraldBindMapper
{
    public LineOfBusiness SupportedLob { get; set; }

    public HeraldBindRequest MapFromGenericBind(GenericBindRequest request, LineOfBusiness lob)
    {
        SupportedLob = lob;
        
        return new HeraldBindRequest
        {
            QuoteReference = request.QuoteId,
            Operation = "BindPolicy",
            PaymentDetails = new HeraldPaymentDetails
            {
                Method = request.Payment?.PaymentMethod ?? "CreditCard",
                Amount = request.Payment?.Amount ?? 0
            }
        };
    }

    public GenericBindResponse MapToGenericResponse(HeraldBindResponse response, LineOfBusiness lob)
    {
        if (response.Status != "Success")
        {
            return new GenericBindResponse
            {
                Success = false,
                Errors = response.Errors?.Select(e => e.Message).ToList() ?? new List<string> { "Bind failed" },
                LineOfBusiness = lob,
                Provider = ProviderType.Herald
            };
        }

        return new GenericBindResponse
        {
            Success = true,
            PolicyNumber = response.PolicyId,
            LineOfBusiness = lob,
            Provider = ProviderType.Herald,
            EffectiveDate = response.EffectiveDate,
            ExpirationDate = response.ExpirationDate,
            BoundPremium = response.Premium,
            Message = "Policy successfully bound with Herald"
        };
    }
}
