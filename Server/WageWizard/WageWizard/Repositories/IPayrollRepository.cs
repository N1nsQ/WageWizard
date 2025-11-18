using WageWizard.DTOs;

namespace WageWizard.Repositories
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<PayrollRatesDto>> GetPayrollRatesAsync();
        Task<SalaryStatementCalculationDto?> CalculateSalaryStatementAsync(Guid employeeId);

    }
}
