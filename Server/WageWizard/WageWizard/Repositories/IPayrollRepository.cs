using WageWizard.Models;
using WageWizard.DTOs;

namespace WageWizard.Repositories
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<PayrollRatesDto>> GetPayrollRatesAsync();
        Task<SalaryCalculationResultsDto?> CalculateSalaryStatementAsync(Guid employeeId);

    }
}
