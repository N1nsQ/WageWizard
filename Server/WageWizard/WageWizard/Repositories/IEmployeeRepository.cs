using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync();
        Task<EmployeeDetailsDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<EmployeesSalaryDetailsDto>> GetEmployeesSalaryPaymentDetailsAsync();
        Task AddAsync(Employee employee);
        Task SaveChangesAsync();
    }
}
