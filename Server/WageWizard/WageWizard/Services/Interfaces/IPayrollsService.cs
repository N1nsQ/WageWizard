using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Services.Interfaces
{
    public interface IPayrollsService
    {
        Task<SalaryStatementCalculationDto> CalculateSalaryStatementAsync(Guid employeeId);
    }
}
