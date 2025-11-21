using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetByIdAsync(Guid id); // Testattu ja tarkistettu
        Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync(); // Testattu ja tarkistettu
        Task<Employee> CreateEmployeeAsync(NewEmployeeRequestDto dto); // Testattu ja tarkistettu
    }
}
