using WageWizard.Domain.Entities;
namespace WageWizard.Repositories
{
    public interface IPayrollsRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(Guid id); 
        Task<PayrollRates?> GetRatesForYearAsync(int year);

    }
}
