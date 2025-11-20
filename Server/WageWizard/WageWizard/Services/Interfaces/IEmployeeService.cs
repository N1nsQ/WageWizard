using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync();
        Task<EmployeeDetailsDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<EmployeesSalaryDetailsDto>> GetEmployeesSalaryPaymentDetailsAsync();
        Task<Employee> CreateEmployeeAsync(NewEmployeeRequestDto dto);
    }
}
