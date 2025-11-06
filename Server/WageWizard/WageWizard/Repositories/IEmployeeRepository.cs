using WageWizard.DTOs;
using WageWizard.Models;

namespace WageWizard.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync();
        Task<EmployeeDetailsDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<EmployeesSalaryDetailsDto>> GetEmployeesSalaryPaymentDetailsAsync();
        Task<EmployeesSalaryDetailsDto?> GetPayrollDetailsByIdAsync(Guid id);

        // To Do:
        // Task AddAsync(Employee employee);
        // Task UpdateAsync(Employee employee);
        // Task DeleteAsync(Guid id);
    }
}
