using WageWizard.DTOs;
using WageWizard.Models;

namespace WageWizard.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync();
        Task<EmployeeDetailsDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<EmployeesSalaryDetailsDto>> GetEmployeesSalaryPaymentDetailsAsync();
    }
}
