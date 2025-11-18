using WageWizard.DTOs;

namespace WageWizard.Repositories.Interfaces
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<PayrollRatesDto>> GetPayrollRatesAsync();
        Task<SalaryStatementCalculationDto?> CalculateSalaryStatementAsync(Guid employeeId);

    }
}
