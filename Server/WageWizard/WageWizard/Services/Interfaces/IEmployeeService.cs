using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync();
        Task<Employee> CreateEmployeeAsync(NewEmployeeRequestDto dto);
    }
}
