using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.DTOs.LOB.WorkerComp;

namespace InsuranceWrapperApi.Domain.Mappers.LOB;

public class WorkerCompMapper : IWorkerCompMapper
{
    public WorkerCompQuoteRequest MapFromGeneric(GenericQuoteRequest request)
    {
        var wcPayrollClasses = request.PayrollByClass?.Select(p => new WCPayrollClass
        {
            ClassCode = p.ClassCode ?? string.Empty,
            ClassDescription = p.ClassDescription ?? string.Empty,
            StateCode = request.StateOfOperation ?? request.BusinessAddress?.State ?? string.Empty,
            AnnualPayroll = p.AnnualPayroll,
            NumberOfEmployees = p.NumberOfEmployees
        }).ToList() ?? new List<WCPayrollClass>();

        return new WorkerCompQuoteRequest
        {
            BusinessName = request.BusinessName ?? string.Empty,
            ContactName = request.ContactName ?? string.Empty,
            Email = request.Email ?? string.Empty,
            Phone = request.Phone ?? string.Empty,
            BusinessAddress = request.BusinessAddress ?? new Address(),
            EffectiveDate = request.EffectiveDate,
            
            // Worker Comp Specific
            FederalEmployerID = string.Empty, // Should be in additional data
            StateOfOperation = request.StateOfOperation ?? request.BusinessAddress?.State ?? string.Empty,
            YearsInBusiness = request.YearsInBusiness ?? 0,
            LegalEntity = "Corporation", // Default
            
            // Payroll
            PayrollByClass = wcPayrollClasses,
            TotalFullTimeEmployees = request.NumberOfEmployees ?? wcPayrollClasses.Sum(p => p.NumberOfEmployees),
            TotalPartTimeEmployees = 0,
            TotalAnnualPayroll = wcPayrollClasses.Sum(p => p.AnnualPayroll),
            
            // Owners
            Owners = new List<WCOwner>(),
            OwnersIncludedInPayroll = false,
            
            // Experience Mod
            HasExperienceMod = false,
            CurrentExperienceMod = null,
            ExperienceModState = null,
            
            // Claims
            HasPriorClaims = request.HasPriorClaims ?? false,
            PriorClaims = new List<WCClaim>(),
            
            // Employers Liability
            EmployersLiabilityNeeded = true,
            ELEachAccidentLimit = 1_000_000,
            ELDiseaseEachEmployeeLimit = 1_000_000,
            ELDiseasePolicyLimit = 1_000_000,
            
            // Subcontractors
            UsesSubcontractors = false,
            AnnualSubcontractorCosts = null,
            SubcontractorsHaveOwnWC = false,
            
            AdditionalInfo = request.AdditionalData
        };
    }
}
