using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Repositories
{
    public interface IPayrollsRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(Guid id); // Testattu ja tarkistettu
        Task<PayrollRates?> GetRatesForYearAsync(int year); // Testattu ja tarkistettu

    }
}
